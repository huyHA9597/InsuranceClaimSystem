using Ardalis.Result;
using MediatR;

namespace InsuranceClaimSystem.API.Features.Claims.GetAllClaimsByStatus
{
    public record GetAllClaimsByStatusRequest(string status) : IRequest<Result<List<ClaimResponse>>>;
}
