using _4oito6.Domain.Specs.Core.Enum;

namespace _4oito6.Domain.Specs.Core.Interfaces
{
    public interface IBusinessSpec
    {
        void AddMessage(BusinessSpecStatus status, string message);

        bool IsSatisfied();
    }
}