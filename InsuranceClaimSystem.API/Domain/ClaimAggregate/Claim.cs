using Microsoft.EntityFrameworkCore;

namespace InsuranceClaimSystem.API.Domain.ClaimAggregate
{
    public class Claim
    {
        public Claim()
        {
        }

        public Claim(string name, string description, float amount, DateTime date, ClaimStatus status)
        {
            Id = Guid.NewGuid();
            CustomerName = name;
            Amount = amount;
            Description = description;
            ClaimDate = date;
            Status = status;
        }

        public Guid Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public float Amount { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime ClaimDate { get; set; }
        public ClaimStatus Status { get; set; }

    }
}
