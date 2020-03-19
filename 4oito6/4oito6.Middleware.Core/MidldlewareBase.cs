using _4oito6.Domain.Application.Core.Contracts.Arguments;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace _4oito6.Middleware.Core
{
    public abstract class MidldlewareBase
    {
        private readonly RequestDelegate _next;

        public MidldlewareBase(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public virtual async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                using var writer = new StreamWriter(context.Response.Body, Encoding.UTF8, 2048, true);
                var response = new ResponseMessage(ex.Message, (int)HttpStatusCode.InternalServerError);

                context.Response.StatusCode = response.StatusCode;
                context.Response.Headers.Add("Content-Type", "application/json");

                await writer.WriteAsync(JsonSerializer.Serialize(response)).ConfigureAwait(false);
            }
        }
    }
}