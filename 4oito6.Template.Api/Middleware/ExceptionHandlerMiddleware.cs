using _4oito6.AuditTrail.Application.Contracts;
using _4oito6.Middleware.Core;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace _4oito6.Template.Api.Middleware
{
    public class ExceptionHandlerMiddleware : MidldlewareBase
    {
        private readonly IAuditTrailAppService _auditTrailAppService;

        public ExceptionHandlerMiddleware(IAuditTrailAppService auditTrailAppService, RequestDelegate next) : base(next)
        {
            _auditTrailAppService = auditTrailAppService ?? throw new ArgumentNullException(nameof(auditTrailAppService));
        }

        public override async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await Next(context).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await TreatExceptionAsync(context, ex).ConfigureAwait(false);
                await _auditTrailAppService.CreateAsync(ex).ConfigureAwait(false);
            }
        }
    }
}