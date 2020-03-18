using _4oito6.Template.Domain.Model.Entities;
using System.Threading.Tasks;

namespace _4oito6.Template.Infra.Data.Bus.Contracts.Interfaces
{
    public interface IUserBus
    {
        Task<User> CreateUser(User user);
    }
}