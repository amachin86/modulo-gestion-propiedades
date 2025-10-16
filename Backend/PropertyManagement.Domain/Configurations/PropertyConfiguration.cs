using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyManagement.Domain.Entities;

namespace PropertyManagement.Domain.Configurations;

public class PropertyConfiguration : IEntityTypeConfiguration<Property>
{
    public void Configure(EntityTypeBuilder<Property> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd();

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.Location)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(p => p.PricePerNight)
              .HasPrecision(10, 2)
              .IsRequired();

        builder.Property(p => p.Status)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasOne(p => p.Host)
            .WithMany(h => h.Properties)
            .HasForeignKey(p => p.HostId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(p => p.HostId, "IX_Property_HostId");

        // CHECK constraint for Status
        //builder.ToTable(t => t.HasCheckConstraint("CK_Property_Status", "Status IN ('Active', 'Inactive')"));
        //builder.HasCheckConstraint("CK_Property_Status", "Status IN ('Active', 'Inactive')");        
    }
}
