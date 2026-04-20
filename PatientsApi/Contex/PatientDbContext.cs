using Microsoft.EntityFrameworkCore;
using PatientsApi.Models;

public class AppDbContext : DbContext
{
    public DbSet<Patient> Patients => Set<Patient>();

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Patient>(entity =>
        {
            entity.ToTable("Patients");

            entity.HasKey(p => p.PatientId);

            entity.Property(p => p.DocumentType)
                .IsRequired()
                .HasMaxLength(10);

            entity.Property(p => p.DocumentNumber)
                .IsRequired()
                .HasMaxLength(20);

            entity.Property(p => p.FirstName)
                .IsRequired()
                .HasMaxLength(80);

            entity.Property(p => p.LastName)
                .IsRequired()
                .HasMaxLength(80);

            entity.Property(p => p.PhoneNumber)
                .HasMaxLength(20);

            entity.Property(p => p.Email)
                .HasMaxLength(120);

            entity.Property(p => p.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            entity.HasIndex(p => new { p.DocumentType, p.DocumentNumber })
                .IsUnique();
        });
    }
}