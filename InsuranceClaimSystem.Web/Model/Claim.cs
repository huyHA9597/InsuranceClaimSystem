namespace InsuranceClaimSystem.Web.Model
{
    public class Claim
    {
        public string customerName { get; set; } = string.Empty;
        public double amount { get; set; }
        public string description { get; set; } = string.Empty;
        public DateTime? createdDate { get; set; }
        public string status { get; set; } = string.Empty;
    }
}
