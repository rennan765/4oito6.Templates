using _4oito6.Infra.Data.Repositories.Core.Implementation;
using _4oito6.Template.Infra.Data.Context;
using _4oito6.Template.Infra.Data.Model.Entities;
using _4oito6.Template.Infra.Data.Repositories.Contracts.Entity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace _4oito6.Template.Infra.Data.Repositories.Implementation.Entity
{
    public class UserRepository : EntityRepositoryBase<User, int>, IUserRepository
    {
        public UserRepository(TemplateContext context) : base(context)
        {
        }

        public override async Task<User> GetByIdAsync(int id)
        {
            return await Context.Set<User>()
                .Include(u => u.Address)
                .Include(u => u.Phones).ThenInclude(up => up.Phone)
                .FirstOrDefaultAsync(u => u.Id == id)
                .ConfigureAwait(false);
        }
    }
}