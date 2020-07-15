using _4oito6.Contact.Domain.Model.Views;
using _4oito6.Contact.Infra.Data.Context.Mapper;
using Microsoft.EntityFrameworkCore;

namespace _4oito6.Contact.Infra.Data.Context
{
    public partial class ContactContext : DbContext
    {
        public DbSet<ViewPhone> ViewPhone { get; set; }
        public DbSet<ViewAddress> ViewAddress { get; set; }

        public ContactContext()
        {
        }

        public ContactContext(DbContextOptions<ContactContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => base.OnConfiguring(optionsBuilder);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyMapping();

            base.OnModelCreating(modelBuilder);
        }
    }
}