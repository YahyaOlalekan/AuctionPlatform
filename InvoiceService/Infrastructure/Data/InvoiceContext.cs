using InvoiceService.Domain.Entities;
using InvoiceService.Infrastructure.Data.Configuration;
using Microsoft.EntityFrameworkCore;

namespace InvoiceService.Infrastructure.Data
{
    public class InvoiceContext : DbContext
    {
        public InvoiceContext(DbContextOptions<InvoiceContext> options) : base(options)
        {
        }

        public DbSet<Invoice> Invoices { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new InvoiceConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }

}
