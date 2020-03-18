using _4oito6.Domain.Model.Core.Entities;
using _4oito6.Domain.Specs.Core.Interfaces;
using System;

namespace _4oito6.Domain.Services.Core.Contracts.Interfaces
{
    public interface IServiceBase : IDisposable
    {
        void AddSpec(IBusinessSpec<EntityBase> businessSpec);

        bool IsSatisfied();
    }
}