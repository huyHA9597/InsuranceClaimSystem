namespace InsuranceClaimSystem.API.Features.Claims
{
    public class ClaimResponse
    {
        public string CustomerName { get; set; } = string.Empty;
        public double Amount { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
