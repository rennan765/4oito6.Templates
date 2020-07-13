using _4oito6.Contact.Domain.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _4oito6.Contact.Infra.Data.Context.Mapper.Configuration
{
    public class ViewPhoneConfiguration : IEntityTypeConfiguration<ViewPhone>
    {
        public void Configure(EntityTypeBuilder<ViewPhone> builder)
        {
            builder.HasNoKey();

            builder.Property(p => p.Id).HasColumnName("id");
            builder.Property(p => p.LocalCode).HasColumnName("ddd");

            builder.Property(p => p.Number).HasColumnName("numero");
            builder.Property(p => p.IdUser).HasColumnName("idusuario");
        }
    }
}