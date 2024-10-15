using InsuranceClaimSystem.API.Domain;

namespace InsuranceClaimSystem.API.Extensions
{
    public class RandomClaimProcessExtension
    {
        public static ClaimStatus ToProcessClaim()
        {
            Random random = new Random();
            float randomNumber = random.Next(0, 2);
            if (randomNumber >= 1)
            {
                return ClaimStatus.Pending;
            }
            else
            {
                return randomNumber >= 0.5 ? ClaimStatus.Approved : ClaimStatus.Rejected;
            }
        }
    }
}
