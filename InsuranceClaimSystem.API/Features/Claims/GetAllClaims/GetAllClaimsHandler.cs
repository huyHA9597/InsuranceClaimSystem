using Ardalis.Result;
using AutoMapper;
using InsuranceClaimSystem.API.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InsuranceClaimSystem.API.Features.Claims.GetAllClaims
{
    public class GetAllClaimsHandler(ApplicationDbContext context, IMapper mapper) : IRequestHandler<GetAllClaimsRequest, Result<List<ClaimResponse>>>
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<Result<List<ClaimResponse>>> Handle(GetAllClaimsRequest request, CancellationToken cancellationToken)
        {
            var claims = await _context.Claims.AsNoTracking().ToListAsync(cancellationToken);

            if (claims == null)
            {
                return Result<List<ClaimResponse>>.NotFound($"No claims saved");
            }
            else
            {
                var result = _mapper.Map<List<ClaimResponse>>(claims);
                return Result<List<ClaimResponse>>.Success(result);
            }
        }
    }
}
