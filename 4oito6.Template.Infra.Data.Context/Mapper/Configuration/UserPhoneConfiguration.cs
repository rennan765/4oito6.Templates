using _4oito6.Template.Infra.Data.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _4oito6.Template.Infra.Data.Context.Mapper.Configuration
{
    public class UserPhoneConfiguration : IEntityTypeConfiguration<UserPhone>
    {
        public void Configure(EntityTypeBuilder<UserPhone> builder)
        {
            builder.ToTable("usuario_telefone");

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.IdPhone).HasColumnName("idtelefone");

            builder.Property(e => e.IdUser).HasColumnName("idusuario");

            builder.HasOne(d => d.Phone)
                .WithMany(p => p.Users)
                .HasForeignKey(d => d.IdPhone)
                .HasConstraintName("telefone_usuario_telefone");

            builder.HasOne(d => d.User)
                .WithMany(p => p.Phones)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("usuario_usuario_telefone");
        }
    }
}