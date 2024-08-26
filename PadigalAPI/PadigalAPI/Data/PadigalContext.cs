using Microsoft.EntityFrameworkCore;
using PadigalAPI.Models;

namespace PadigalAPI.Data
{
    public class PadigalContext: DbContext
    {
        public PadigalContext(DbContextOptions<PadigalContext> options)
       : base(options)
        {
        }

        public DbSet<Venta> Ventas { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Venta>()
                .Property(v => v.Precio)
                .HasColumnType("decimal(18,2)"); // Especifica la precisión y escala

            base.OnModelCreating(modelBuilder);
        }
    }
}
