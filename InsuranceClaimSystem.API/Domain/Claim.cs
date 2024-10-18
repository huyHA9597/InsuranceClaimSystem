namespace InsuranceClaimSystem.API.Domain
{
    public class Claim
    {
        public Claim()
        {
        }

        public Claim(string name, string description, double amount, DateTime date, ClaimStatus status)
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
        public double Amount { get; set; }      // To save any number that have decimal part.
        public string Description { get; set; } = string.Empty;
        public DateTime ClaimDate { get; set; }
        public ClaimStatus Status { get; set; }
    }
}
