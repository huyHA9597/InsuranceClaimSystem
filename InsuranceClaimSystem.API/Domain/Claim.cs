namespace InsuranceClaimSystem.API.Domain
{
    public class Claim
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public float Amount { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime ClaimDate { get; set; }
        public ClaimStatus Status { get; set; }
    }

    public enum ClaimStatus
    {
        Pending,
        Approved,
        Rejected
    }
}
