using System;

namespace _4oito6.AuditTrail.Domain.Services.Contract.Arguments
{
    public class AuditTrailResponse
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }

        public static explicit operator AuditTrailResponse(Entities.AuditTrail auditTrail)
            => new AuditTrailResponse
            {
                Id = auditTrail.Id,
                Date = auditTrail.Date,
                Message = auditTrail.Message,
                StackTrace = auditTrail.StackTrace
            };
    }
}