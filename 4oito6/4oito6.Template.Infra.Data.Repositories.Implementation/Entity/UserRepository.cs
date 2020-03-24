using _4oito6.Infra.Data.Repositories.Core.Implementation;
using _4oito6.Template.Infra.Data.Context;
using _4oito6.Template.Infra.Data.Model.Entities;
using _4oito6.Template.Infra.Data.Repositories.Contracts.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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
            var user = await Context.Set<User>().FindAsync(id).ConfigureAwait(false);

            user.Phones = await Context.Set<UserPhone>()
                .Include(up => up.Phone)
                .Where(up => up.IdUser == id)
                .ToListAsync()
                .ConfigureAwait(false);

            if (user.IdAddress != null)
                user.Address = await Context.Set<Address>()
                    .FindAsync(user.IdAddress)
                    .ConfigureAwait(false);

            return user;
        }
    }
}