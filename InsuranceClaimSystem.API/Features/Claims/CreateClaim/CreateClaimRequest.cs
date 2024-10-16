using Ardalis.Result;
using MediatR;

namespace InsuranceClaimSystem.API.Features.Claims.CreateClaim
{
    public class CreateClaimRequest : IRequest<Result<Guid>>
    {
        public CreateClaimRequest(string name, string description, float amount, DateTime createdDate)
        {
            Name = name;
            Description = description;
            Amount = amount;
            CreatedDate = createdDate;
        }

        public CreateClaimRequest() { }

        public string Name { get; init; }
        public string Description { get; init; }
        public float Amount { get; init; }
        public DateTime CreatedDate { get; init; }
    }
}
