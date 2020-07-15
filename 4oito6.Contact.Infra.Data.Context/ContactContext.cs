using _4oito6.Contact.Domain.Model.Views;
using _4oito6.Contact.Infra.Data.Context.Mapper;
using _4oito6.Template.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace _4oito6.Contact.Infra.Data.Context
{
    public partial class ContactContext : TemplateContext
    {
        public DbSet<ViewPhone> ViewPhone { get; set; }
        public DbSet<ViewAddress> ViewAddress { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => base.OnConfiguring(optionsBuilder);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyMapping();

            base.OnModelCreating(modelBuilder);
        }
    }
}