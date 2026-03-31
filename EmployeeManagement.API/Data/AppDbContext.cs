using EmployeeManagement.API.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);

                entity.Property(e => e.FullName)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.Email)
                      .IsRequired()
                      .HasMaxLength(150);

                entity.HasIndex(e => e.Email)
                      .IsUnique();

                entity.Property(e => e.PhoneNumber)
                      .IsRequired()
                      .HasMaxLength(15);

                entity.Property(e => e.Designation)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.Department)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(e => e.Status)
                      .IsRequired()
                      .HasMaxLength(10)
                      .HasDefaultValue("Active");

                entity.Property(e => e.Location)
                      .HasMaxLength(100);

                entity.Property(e => e.SalaryBand)
                      .HasMaxLength(50);
            });
        }
    }
}