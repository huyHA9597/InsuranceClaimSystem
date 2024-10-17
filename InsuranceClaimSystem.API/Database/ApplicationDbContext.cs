using InsuranceClaimSystem.API.Domain;
using Microsoft.EntityFrameworkCore;

namespace InsuranceClaimSystem.API.Database
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Claim> Claims { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed data
            modelBuilder.Entity<Claim>().HasData(new Claim
            {
                Id = Guid.NewGuid(),
                CustomerName = "Huy",
                Description = "Test claim 1",
                Amount = 100f,
                ClaimDate = DateTime.Now.Date,
                Status = ClaimStatus.Pending
            });

            modelBuilder.Entity<Claim>().HasData(new Claim
            {
                Id = Guid.NewGuid(),
                CustomerName = "Adam",
                Description = "Test claim 2",
                Amount = Math.Round(20.4f, 2),
                ClaimDate = DateTime.Now.Date,
                Status = ClaimStatus.Pending
            });

            modelBuilder.Entity<Claim>().HasData(new Claim
            {
                Id = Guid.NewGuid(),
                CustomerName = "Ben",
                Description = "Test claim 3",
                Amount = 15f,
                ClaimDate = DateTime.Now.Date,
                Status = ClaimStatus.Rejected
            });

            modelBuilder.Entity<Claim>().HasData(new Claim
            {
                Id = Guid.NewGuid(),
                CustomerName = "Lily",
                Description = "Test claim 4",
                Amount = 19f,
                ClaimDate = DateTime.Now.Date,
                Status = ClaimStatus.Approved
            });
        }
    }
}
