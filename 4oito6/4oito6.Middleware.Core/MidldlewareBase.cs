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
        protected readonly RequestDelegate Next;

        public MidldlewareBase(RequestDelegate next)
        {
            Next = next ?? throw new ArgumentNullException(nameof(next));
        }

        protected async Task TreatExceptionAsync(HttpContext context, Exception ex)
        {
            using var writer = new StreamWriter(context.Response.Body, Encoding.UTF8, 2048, true);
            var response = new ResponseMessage(ex.Message, (int)HttpStatusCode.InternalServerError);

            context.Response.StatusCode = response.StatusCode;
            context.Response.Headers.Add("Content-Type", "application/json");

            await writer.WriteAsync(JsonSerializer.Serialize(response)).ConfigureAwait(false);
        }

        public virtual async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await Next(context).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await TreatExceptionAsync(context, ex).ConfigureAwait(false);
            }
        }
    }
}