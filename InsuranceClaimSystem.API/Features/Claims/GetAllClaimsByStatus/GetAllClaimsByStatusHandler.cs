using AutoMapper;
using InsuranceClaimSystem.API.Database;
using MediatR;
using Ardalis.Result;
using InsuranceClaimSystem.API.Domain.ClaimAggregate;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace InsuranceClaimSystem.API.Features.Claims.GetAllClaimsByStatus
{
    public class GetAllClaimsByStatusHandler(ApplicationDbContext context, IMapper mapper) : IRequestHandler<GetAllClaimsByStatusRequest, Result<List<ClaimResponse>>>
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<List<ClaimResponse>>> Handle(GetAllClaimsByStatusRequest request, CancellationToken cancellationToken)
        {
            var filterStatus = GetClaimStatus(request.status);
            if (filterStatus == null)
            {
                return Result<List<ClaimResponse>>.NotFound($"No claims saved with status {request.status.ToString()}");
            }
            else
            {
                var filterClaims = await _context.Claims.AsNoTracking().Where(s => s.Status == filterStatus).ToListAsync(cancellationToken);
                return Result<List<ClaimResponse>>.Success(_mapper.Map<List<ClaimResponse>>(filterClaims));
            }
        }

        private static ClaimStatus? GetClaimStatus(string input)
        {
            if (Enum.TryParse(input, true, out ClaimStatus claimStatus))
            {
                return claimStatus;
            }
            else
            {
                return null;
            }
        }
    }
}
