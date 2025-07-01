using fs_mvc_with_auth_2025_07_01.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace fs_mvc_with_auth_2025_07_01.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public DbSet<DoctorPatientModel> DoctorPatient { get; set; } = default!;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

            

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Many-to-many relationship configuration
            builder.Entity<DoctorPatientModel>()
                .HasKey(dp => new { dp.DoctorId, dp.PatientId }); // Composite primary key

            builder.Entity<DoctorPatientModel>()
                .HasOne(dp => dp.Doctor)
                .WithMany()
                .HasForeignKey(dp => dp.DoctorId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletes

            builder.Entity<DoctorPatientModel>()
                .HasOne(dp => dp.Patient)
                .WithMany()
                .HasForeignKey(dp => dp.PatientId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletes
        }
    }
}
