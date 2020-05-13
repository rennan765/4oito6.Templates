using _4oito6.Middleware.Core;
using Microsoft.AspNetCore.Http;

namespace _4oito6.Template.Api.Middleware
{
    public class ExceptionHandlerMiddleware : MidldlewareBase
    {
        public ExceptionHandlerMiddleware(RequestDelegate next) : base(next)
        {
        }
    }
}