using _4oito6.Domain.Specs.Core.Interfaces;
using System;
using System.Net;

namespace _4oito6.Domain.Services.Core.Contracts.Interfaces
{
    public interface IServiceBase : IDisposable
    {
        void AddSpec(IBusinessSpec businessSpec);

        bool IsSatisfied();

        string[] GetMessages();

        HttpStatusCode GetStatusCode();
    }
}