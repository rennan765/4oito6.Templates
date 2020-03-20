using _4oito6.Domain.Specs.Core.Enum;
using _4oito6.Domain.Specs.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace _4oito6.Domain.Specs.Core.Extensions
{
    public static class BusinessSpecExtensions
    {
        public static BusinessSpecStatus ToBusinessSpecStatus(this IBusinessSpec spec)
            => spec.Messages.Select(m => m.Status).ToList().ToMajorStatus();

        public static HttpStatusCode ToHttpStatusCode(this IList<IBusinessSpec> specs)
            => specs.Select(s => s.ToBusinessSpecStatus()).ToList().ToMajorStatus().ToHttpStatusCode();
    }
}