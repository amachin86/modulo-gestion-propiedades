using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PropertyManagement.Domain.Entities;

namespace PropertyManagement.Domain.Configurations;

public class DomainEventConfiguration : IEntityTypeConfiguration<DomainEvent>
{
    public void Configure(EntityTypeBuilder<DomainEvent> builder)
    {
        builder.HasKey(de => de.Id);
        builder.Property(de => de.Id).ValueGeneratedOnAdd();

        builder.Property(de => de.EventType)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(de => de.PayloadJSON)
            .IsRequired();

        builder.HasOne(de => de.Property)
            .WithMany(p => p.DomainEvents)
            .HasForeignKey(de => de.PropertyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(de => de.PropertyId, "IX_DomainEvent_PropertyId");
    }
}
