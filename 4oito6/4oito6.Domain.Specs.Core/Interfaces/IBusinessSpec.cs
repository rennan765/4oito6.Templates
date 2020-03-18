using _4oito6.Domain.Model.Core.Entities;
using _4oito6.Domain.Specs.Core.Enum;

namespace _4oito6.Domain.Specs.Core.Interfaces
{
    public interface IBusinessSpec<TEntity>
        where TEntity : EntityBase
    {
        void AddMessage(BusinessSpecStatus status, string message);

        bool IsSatisfied();
    }
}