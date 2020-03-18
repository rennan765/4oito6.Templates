using _4oito6.Infra.Data.Context.Mapper.Configuration;
using Microsoft.EntityFrameworkCore;

namespace _4oito6.Infra.Data.Context.Mapper
{
    public static class MapperConfiguration
    {
        public static ModelBuilder ApplyMapping(this ModelBuilder modelBuilder)
        {
            return modelBuilder
                .ApplyConfiguration(new AddressConfiguration())
                .ApplyConfiguration(new PhoneConfiguration())
                .ApplyConfiguration(new UserConfiguration())
                .ApplyConfiguration(new UserPhoneConfiguration());
        }
    }
}