using _4oito6.AuditTrail.Application.Contracts;
using _4oito6.Domain.Application.Core.Contracts.Arguments;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace _4oito6.AuditTrail.Middleware
{
    public class AuditTrailMiddleware
    {
        private readonly RequestDelegate _next;

        public AuditTrailMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        private async Task TreatExceptionAsync(HttpContext context, Exception ex)
        {
            var message = string.Empty;

#if DEBUG
            message = ex.Message;
#else
            message = "Internal Server Error";
#endif

            using var writer = new StreamWriter(context.Response.Body, Encoding.UTF8, 2048, true);
            var response = new ResponseMessage(ex.Message, (int)HttpStatusCode.InternalServerError);

            context.Response.StatusCode = response.StatusCode;
            context.Response.Headers.Add("Content-Type", "application/json");

            await writer.WriteAsync(JsonSerializer.Serialize(response)).ConfigureAwait(false);
        }

        public async Task InvokeAsync(HttpContext context, [FromServices] IAuditTrailAppService auditTrailAppService)
        {
            try
            {
                await _next(context).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await TreatExceptionAsync(context, ex).ConfigureAwait(false);
                await auditTrailAppService.CreateAsync(ex).ConfigureAwait(false);
            }
        }
    }
}