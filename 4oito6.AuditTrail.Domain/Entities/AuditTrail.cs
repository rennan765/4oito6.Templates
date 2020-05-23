using System;

namespace _4oito6.AuditTrail.Domain.Entities
{
    public sealed class AuditTrail
    {
        public string Id { get; private set; }
        public DateTime Date { get; private set; }
        public string Message { get; private set; }
        public string StackTrace { get; private set; }

        /// <summary>
        /// Full constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="date"></param>
        /// <param name="message"></param>
        /// <param name="stackTrace"></param>
        public AuditTrail(string id, DateTime date, string message, string stackTrace)
        {
            Id = id;
            Date = date;
            Message = message;
            StackTrace = stackTrace;
        }

        /// <summary>
        /// Create a new AuditTrail
        /// </summary>
        /// <param name="exception"></param>
        public AuditTrail(Exception exception, DateTime? date = null)
        {
            Id = new Random().Next().ToString();
            Date = date ?? DateTime.UtcNow;
            Message = exception.Message;
            StackTrace = exception.StackTrace;
        }
    }
}