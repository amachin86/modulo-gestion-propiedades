using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyManagement.Domain.Entities;

namespace PropertyManagement.Domain.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).ValueGeneratedOnAdd();

        builder.Property(b => b.CheckIn)
            .IsRequired();

        builder.Property(b => b.CheckOut)
            .IsRequired();

        builder.Property(b => b.TotalPrice)
                 .HasPrecision(10, 2)
                 .IsRequired();

        builder.HasOne(b => b.Property)
            .WithMany(p => p.Bookings)
            .HasForeignKey(b => b.PropertyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(b => b.PropertyId, "IX_Booking_PropertyId");
    }
}
