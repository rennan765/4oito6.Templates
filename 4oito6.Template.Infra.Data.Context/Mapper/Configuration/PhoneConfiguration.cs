using _4oito6.Template.Infra.Data.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _4oito6.Template.Infra.Data.Context.Mapper.Configuration
{
    public class PhoneConfiguration : IEntityTypeConfiguration<Phone>
    {
        public void Configure(EntityTypeBuilder<Phone> builder)
        {
            builder.ToTable("telefone");

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.LocalCode)
                .IsRequired()
                .HasColumnName("ddd")
                .HasMaxLength(2)
                .IsFixedLength();

            builder.Property(e => e.Number)
                .IsRequired()
                .HasColumnName("numero")
                .HasMaxLength(9);
        }
    }
}