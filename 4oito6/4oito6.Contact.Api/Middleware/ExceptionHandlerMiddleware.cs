using _4oito6.Middleware.Core;
using Microsoft.AspNetCore.Http;

namespace _4oito6.Contact.Api.Middleware
{
    public class ExceptionHandlerMiddleware : MidldlewareBase
    {
        public ExceptionHandlerMiddleware(RequestDelegate next) : base(next)
        {
        }
    }
}