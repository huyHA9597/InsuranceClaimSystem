using Ardalis.Result;
using MediatR;

namespace InsuranceClaimSystem.API.Features.Claims.GetAllClaims
{
    public record GetAllClaimsRequest() : IRequest<Result<List<ClaimResponse>>>;
}
