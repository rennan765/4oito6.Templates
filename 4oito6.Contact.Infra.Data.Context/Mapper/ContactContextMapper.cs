using _4oito6.Contact.Infra.Data.Context.Mapper.Configuration;
using Microsoft.EntityFrameworkCore;

namespace _4oito6.Contact.Infra.Data.Context.Mapper
{
    public static class ContactContextMapper
    {
        public static ModelBuilder ApplyMapping(this ModelBuilder modelBuilder)
        {
            return modelBuilder
                .ApplyConfiguration(new ViewAddressConfiguration())
                .ApplyConfiguration(new ViewPhoneConfiguration());
        }
    }
}