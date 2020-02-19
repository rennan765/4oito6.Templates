using Microsoft.AspNetCore.Http;
using System;
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
                throw;
            }
        }
    }
}