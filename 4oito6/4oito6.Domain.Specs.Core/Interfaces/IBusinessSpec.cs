using _4oito6.Domain.Specs.Core.Enum;
using _4oito6.Domain.Specs.Core.Models;
using System.Collections.Generic;
using System.Net;

namespace _4oito6.Domain.Specs.Core.Interfaces
{
    public interface IBusinessSpec
    {
        IList<BusinessSpecMessage> Messages { get; }

        void AddMessage(BusinessSpecStatus status, string message);

        bool IsSatisfied();

        HttpStatusCode GetStatusCode();
    }
}