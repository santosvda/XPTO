using System.Linq;
using Microsoft.EntityFrameworkCore;
using XPTO.Domain;
using XPTO.Repository.Configuration;

namespace XPTO.Repository
{
    public class XPTOContext : DbContext
    {
        public XPTOContext(DbContextOptions<XPTOContext> options) : base (options){ }
        public DbSet<Product> Products { get; set; }
         protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                // EF Core 1 & 2
                property.Relational().ColumnType = "decimal(10, 2)";

                // EF Core 3
                //property.SetColumnType("decimal(18, 6)");

                // EF Core 5
                //property.SetPrecision(18);
                //property.SetScale(6);
            }
        }
    }
}