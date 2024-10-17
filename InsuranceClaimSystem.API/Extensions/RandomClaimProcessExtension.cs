using InsuranceClaimSystem.API.Domain;

namespace InsuranceClaimSystem.API.Extensions
{
    public class RandomClaimProcessExtension
    {
        // To return the claim status randomly using a random generated number.
        // If the random number is greater than 1, then the status will be Pending.
        // Otherwise, depend on the random number, if greater than 0.5, then the status will be Approved.
        public static ClaimStatus ToProcessClaim()
        {
            Random random = new Random();
            double randomNumber = random.NextDouble() * 2;
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
