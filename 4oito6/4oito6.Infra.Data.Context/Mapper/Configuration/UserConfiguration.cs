using _4oito6.Template.Infra.Data.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _4oito6.Infra.Data.Context.Mapper.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("usuario");

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.Cpf)
                .HasColumnName("cpf")
                .HasMaxLength(11)
                .IsFixedLength();

            builder.Property(e => e.Email)
                .IsRequired()
                .HasColumnName("email")
                .HasMaxLength(300);

            builder.Property(e => e.IdAddress).HasColumnName("idendereco");

            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasColumnName("primeironome")
                .HasMaxLength(50);

            builder.Property(e => e.MiddleName)
                .HasColumnName("segundonome")
                .HasMaxLength(50);

            builder.Property(e => e.LastName)
                .IsRequired()
                .HasColumnName("ultimonome")
                .HasMaxLength(50);

            builder.HasOne(d => d.Address)
                .WithMany(p => p.Users)
                .HasForeignKey(d => d.IdAddress)
                .HasConstraintName("usuario_endereco");
        }
    }
}