using _4oito6.Domain.Specs.Core.Enum;

namespace _4oito6.Domain.Specs.Core.Extensions
{
    public static class BusinessSpecStatusExtensions
    {
        public static int FirstCodeNumber(this BusinessSpecStatus status)
        {
            switch (status)
            {
                case BusinessSpecStatus.Success:
                case BusinessSpecStatus.Created:
                case BusinessSpecStatus.Accepted:
                case BusinessSpecStatus.NoContent:
                    return 2;

                case BusinessSpecStatus.InvalidInputs:
                case BusinessSpecStatus.Unauthorized:
                case BusinessSpecStatus.PaymentRequired:
                case BusinessSpecStatus.Forbidden:
                case BusinessSpecStatus.ResourceNotFound:
                case BusinessSpecStatus.Conflict:
                case BusinessSpecStatus.Locked:
                    return 4;

                case BusinessSpecStatus.InternalError:
                case BusinessSpecStatus.Badgateway:
                case BusinessSpecStatus.ServiceUnavailable:
                    return 5;

                default:
                    return 5;
            }
        }
    }
}