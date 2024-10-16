using InsuranceClaimSystem.API.Domain.ClaimAggregate;
using Microsoft.EntityFrameworkCore;

namespace InsuranceClaimSystem.API.Database
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Claim> Claims { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
