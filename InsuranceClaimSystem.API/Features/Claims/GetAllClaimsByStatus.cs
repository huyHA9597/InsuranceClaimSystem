using Carter;
using InsuranceClaimSystem.API.Contracts.Claim;
using InsuranceClaimSystem.API.Database;
using InsuranceClaimSystem.API.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static InsuranceClaimSystem.API.Features.Claims.GetAllClaims;

namespace InsuranceClaimSystem.API.Features.Claims
{
    public static class GetAllClaimsByStatus
    {
        public sealed class GetAllClaimsByStatusQuery : IRequest<List<GetAllClaimsResponse>>
        {
            public ClaimStatus Status { get; set; }
        }

        internal sealed class Handler : IRequestHandler<GetAllClaimsByStatusQuery, List<GetAllClaimsResponse>>
        {
            private readonly ApplicationDbContext _dbContext;

            public Handler(ApplicationDbContext dbContext) => _dbContext = dbContext;

            public async Task<List<GetAllClaimsResponse>> Handle(GetAllClaimsByStatusQuery request, CancellationToken cancellationToken)
                => await _dbContext.Claims.AsNoTracking()
                .Where(c => c.Status == request.Status)
                .Select(q => new GetAllClaimsResponse
                {
                    CustomerName = q.CustomerName,
                    Amount = q.Amount,
                    Description = q.Description,
                    CreatedDate = q.ClaimDate,
                    Status = q.Status.ToString()
                }).ToListAsync(cancellationToken);
        }
    }

    public class GetAllClaimsByStatusEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("api/claims/status", async (ClaimStatus status, ISender sender) =>
            {
                var query = new GetAllClaimsByStatus.GetAllClaimsByStatusQuery { Status = status };
                var result = await sender.Send(query);

                if (result != null)
                {
                    return Results.Ok(result);
                }

                return Results.NotFound();
            });
        }
    }
}
