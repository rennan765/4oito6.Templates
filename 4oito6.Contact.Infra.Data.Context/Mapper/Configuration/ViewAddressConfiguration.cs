using _4oito6.Contact.Domain.Model.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _4oito6.Contact.Infra.Data.Context.Mapper.Configuration
{
    public class ViewAddressConfiguration : IEntityTypeConfiguration<ViewAddress>
    {
        public void Configure(EntityTypeBuilder<ViewAddress> builder)
        {
            builder.HasNoKey();

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.District).HasColumnName("bairro");
            builder.Property(e => e.PostalCode).HasColumnName("cep");

            builder.Property(e => e.City).HasColumnName("cidade");
            builder.Property(e => e.Complement).HasColumnName("complemento");

            builder.Property(e => e.State).HasColumnName("estado");
            builder.Property(e => e.Street).HasColumnName("logradouro");

            builder.Property(e => e.Number).HasColumnName("numero");
            builder.Property(e => e.IdUser).HasColumnName("idusuario");
        }
    }
}