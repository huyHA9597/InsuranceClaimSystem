using InsuranceClaimSystem.API.Domain;
using Microsoft.EntityFrameworkCore;

namespace InsuranceClaimSystem.API.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Claim> Claims { get; set; }
    }
}
