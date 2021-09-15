using Microsoft.EntityFrameworkCore;
using XPTO.Domain;
namespace XPTO.Repository.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Title).HasMaxLength(200);
            builder.Property(p => p.BarCode).HasMaxLength(200);

            builder.Property(p => p.Active).HasDefaultValue(true);
            builder.HasIndex(p => p.Active).IsUnique(false);
        }
    }
}