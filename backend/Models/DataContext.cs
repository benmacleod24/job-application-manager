using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace JPTBackend.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JobApplication>()
                .HasOne(a => a.Resume)
                .WithMany(r => r.Applications)
                .HasForeignKey(a => a.ResumeId)
                .OnDelete(DeleteBehavior.Cascade);  // Adjust as necessary
        }

        public DbSet<Resume> Resumes { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
    }
}
