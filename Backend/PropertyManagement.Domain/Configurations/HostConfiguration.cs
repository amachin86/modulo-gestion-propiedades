using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyManagement.Domain.Entities;

namespace PropertyManagement.Domain.Configurations;

public class HostConfiguration : IEntityTypeConfiguration<Host>
{
    public void Configure(EntityTypeBuilder<Host> builder)
    {
        builder.HasKey(h => h.Id);
        builder.Property(h => h.Id).ValueGeneratedOnAdd();

        builder.Property(h => h.FullName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(h => h.Email)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(h => h.Phone)
            .HasMaxLength(20);     

        builder.HasIndex(h => h.Email).IsUnique();
    }
}
