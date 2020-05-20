using _4oito6.Domain.Specs.Core.Enum;
using System.Collections.Generic;
using System.Linq;
using System.Net;

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

        public static HttpStatusCode ToHttpStatusCode(this BusinessSpecStatus status)
            => (HttpStatusCode)status;

        public static int ToInt(this BusinessSpecStatus status) => (int)status;

        public static BusinessSpecStatus ToBusinessSpecStatus(this int status) => (BusinessSpecStatus)status;

        public static BusinessSpecStatus ToMajorStatus(this IList<BusinessSpecStatus> status)
        {
            if (status.Any(m => m.FirstCodeNumber() == 5))
            {
                if (status.Any(m => m == BusinessSpecStatus.ServiceUnavailable))
                    return BusinessSpecStatus.ServiceUnavailable;
                if (status.Any(m => m == BusinessSpecStatus.Badgateway))
                    return BusinessSpecStatus.Badgateway;

                return BusinessSpecStatus.InternalError;
            }

            if (status.Any(m => m.FirstCodeNumber() == 4))
            {
                if (status.Any(m => m == BusinessSpecStatus.ResourceNotFound))
                    return BusinessSpecStatus.ResourceNotFound;

                if (status.Any(m => m == BusinessSpecStatus.PaymentRequired))
                    return BusinessSpecStatus.PaymentRequired;

                return status
                    .Where(m => m != BusinessSpecStatus.ResourceNotFound && m != BusinessSpecStatus.PaymentRequired)
                    .Select(m => m).Max(m => m.ToInt()).ToBusinessSpecStatus();
            }

            if (status.Any(m => m == BusinessSpecStatus.Success))
                return BusinessSpecStatus.Success;

            return status.Select(m => m).Max(m => m.ToInt()).ToBusinessSpecStatus();
        }
    }
}