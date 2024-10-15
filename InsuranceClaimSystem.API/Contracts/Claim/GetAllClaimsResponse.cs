namespace InsuranceClaimSystem.API.Contracts.Claim
{
    public class GetAllClaimsResponse
    {
        public string CustomerName { get; set; } = string.Empty;
        public double Amount { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
