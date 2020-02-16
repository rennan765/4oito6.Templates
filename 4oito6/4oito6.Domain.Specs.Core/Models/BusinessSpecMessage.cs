using _4oito6.Domain.Specs.Core.Enum;

namespace _4oito6.Domain.Specs.Core.Models
{
    public class BusinessSpecMessage
    {
        public BusinessSpecStatus Status { get; private set; }
        public string Message { get; private set; }

        public BusinessSpecMessage(BusinessSpecStatus status, string message)
        {
            Status = status;
            Message = message;
        }
    }
}