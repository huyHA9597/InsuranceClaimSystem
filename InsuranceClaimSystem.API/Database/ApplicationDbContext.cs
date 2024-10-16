using InsuranceClaimSystem.API.Domain.ClaimAggregate;
using Microsoft.EntityFrameworkCore;

namespace InsuranceClaimSystem.API.Database
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Claim> Claims { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Claim>().HasData(new Claim
            {
                Id = Guid.NewGuid(),
                CustomerName = "Huy",
                Description = "Test claim 1",
                Amount = 100f,
                ClaimDate = DateTime.Now,
                Status = ClaimStatus.Pending
            });

            modelBuilder.Entity<Claim>().HasData(new Claim
            {
                Id = Guid.NewGuid(),
                CustomerName = "Adam",
                Description = "Test claim 2",
                Amount = 20.4f,
                ClaimDate = DateTime.Now,
                Status = ClaimStatus.Pending
            });

            modelBuilder.Entity<Claim>().HasData(new Claim
            {
                Id = Guid.NewGuid(),
                CustomerName = "Ben",
                Description = "Test claim 3",
                Amount = 15f,
                ClaimDate = DateTime.Now,
                Status = ClaimStatus.Rejected
            });

            modelBuilder.Entity<Claim>().HasData(new Claim
            {
                Id = Guid.NewGuid(),
                CustomerName = "Lily",
                Description = "Test claim 4",
                Amount = 19f,
                ClaimDate = DateTime.Now,
                Status = ClaimStatus.Approved
            });
        }
    }
}
