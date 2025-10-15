using Microsoft.EntityFrameworkCore;
using PropertyManagement.Domain.Entities;

namespace PropertyManagement.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Property> Properties { get; set; }
    public DbSet<Host> Hosts { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<DomainEvent> DomainEvents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Property configuration
        modelBuilder.Entity<Property>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Name).IsRequired().HasMaxLength(200);
            entity.Property(p => p.Description).HasMaxLength(1000);
            entity.Property(p => p.Status).IsRequired().HasMaxLength(50);
            entity.Property(p => p.CreatedAt).HasDefaultValueSql("NOW() AT TIME ZONE 'UTC'");
            entity.Property(p => p.UpdatedAt).HasDefaultValueSql("NOW() AT TIME ZONE 'UTC'");

            entity.HasOne(p => p.Host)
                  .WithMany(h => h.Properties)
                  .HasForeignKey(p => p.HostId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(p => p.HostId, "IX_Property_HostId");
        });

        // Host configuration
        modelBuilder.Entity<Host>(entity =>
        {
            entity.HasKey(h => h.Id);
            entity.Property(h => h.Name).IsRequired().HasMaxLength(200);
            entity.Property(h => h.Email).IsRequired().HasMaxLength(200);
            entity.Property(h => h.Phone).HasMaxLength(20);
            entity.Property(h => h.CreatedAt).HasDefaultValueSql("NOW() AT TIME ZONE 'UTC'");

            entity.HasIndex(h => h.Email).IsUnique();
        });

        // User configuration
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Username).IsRequired().HasMaxLength(100);
            entity.Property(u => u.Email).IsRequired().HasMaxLength(200);
            entity.Property(u => u.PasswordHash).IsRequired();
            entity.Property(u => u.Role).IsRequired().HasMaxLength(50);
            entity.Property(u => u.CreatedAt).HasDefaultValueSql("NOW() AT TIME ZONE 'UTC'");

            entity.HasIndex(u => u.Username).IsUnique();
            entity.HasIndex(u => u.Email).IsUnique();
        });

        // DomainEvent configuration
        modelBuilder.Entity<DomainEvent>(entity =>
        {
            entity.HasKey(de => de.Id);
            entity.Property(de => de.EventType).IsRequired().HasMaxLength(100);
            entity.Property(de => de.EventData).IsRequired();
            entity.Property(de => de.OccurredAt).HasDefaultValueSql("NOW() AT TIME ZONE 'UTC'");

            entity.HasOne(de => de.Property)
                  .WithMany(p => p.DomainEvents)
                  .HasForeignKey(de => de.PropertyId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
