using System;

namespace _4oito6.AuditTrail.Infra.Mongo.Repositories.Contracts.Model
{
    public sealed class AuditTrailDto
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
    }
}