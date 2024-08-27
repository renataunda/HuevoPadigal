using Microsoft.EntityFrameworkCore;
using PadigalAPI.Models;

namespace PadigalAPI.Data
{
    public class PadigalContext : DbContext
    {
        public PadigalContext(DbContextOptions<PadigalContext> options)
       : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<ClientAddress> ClientAddresses { get; set; }
        public DbSet<ClientPhone> ClientPhones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Client>()
        .HasMany(c => c.Addresses)
        .WithOne(a => a.Client)
        .HasForeignKey(a => a.ClientId);

            modelBuilder.Entity<Client>()
                .HasMany(c => c.PhoneNumbers)
                .WithOne(p => p.Client)
                .HasForeignKey(p => p.ClientId);

            modelBuilder.Entity<Sale>()
                .HasOne(s => s.Client)
                .WithMany() // Assuming a sale is associated with one client only
                .HasForeignKey(s => s.ClientId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
