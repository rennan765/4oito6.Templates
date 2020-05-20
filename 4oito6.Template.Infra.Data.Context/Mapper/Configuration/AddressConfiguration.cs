using _4oito6.Template.Infra.Data.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _4oito6.Template.Infra.Data.Context.Mapper.Configuration
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("endereco");

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.District)
                .IsRequired()
                .HasColumnName("bairro")
                .HasMaxLength(50);

            builder.Property(e => e.PostalCode)
                .HasColumnName("cep")
                .HasMaxLength(8)
                .IsFixedLength();

            builder.Property(e => e.City)
                .IsRequired()
                .HasColumnName("cidade")
                .HasMaxLength(50);

            builder.Property(e => e.Complement)
                .HasColumnName("complemento")
                .HasMaxLength(100);

            builder.Property(e => e.State)
                .IsRequired()
                .HasColumnName("estado")
                .HasMaxLength(2)
                .IsFixedLength();

            builder.Property(e => e.Street)
                .IsRequired()
                .HasColumnName("logradouro")
                .HasMaxLength(150);

            builder.Property(e => e.Number)
                .HasColumnName("numero")
                .HasMaxLength(10);
        }
    }
}