using _4oito6.Infra.Data.Bus.Core.Contracts;
using _4oito6.Template.Domain.Model.Entities;
using System.Threading.Tasks;

namespace _4oito6.Template.Infra.Data.Bus.Contracts.Interfaces
{
    public interface IUserBus : IBusBase
    {
        Task<User> CreateUserAsync(User user);

        Task<User> UpdateUserAsync(User user);

        Task<bool> ExistsEmailAsync(string email, int? idUser = null);

        Task<User> GetByIdAsync(int id);
    }
}