using Ardalis.Result;
using MediatR;

namespace InsuranceClaimSystem.API.Features.Claims.UpdateClaimStatus
{
    public class UpdateClaimStatusRequest : IRequest<Result<string>>
    {
        public UpdateClaimStatusRequest(string name, double amount, DateTime createdDate)
        {
            Name = name;
            Amount = amount;
            CreatedDate = createdDate;
        }

        public UpdateClaimStatusRequest() { }

        public string Name { get; init; } = string.Empty;
        public double Amount { get; init; }
        public DateTime CreatedDate { get; init; }
    }
}
