using Carter;
using InsuranceClaimSystem.API.Contracts.Claim;
using InsuranceClaimSystem.API.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InsuranceClaimSystem.API.Features.Claims
{
    public static class GetAllClaims
    {
        public sealed class GetAllClaimsQuery : IRequest<List<GetAllClaimsResponse>>
        {
        }

        internal sealed class Handler : IRequestHandler<GetAllClaimsQuery, List<GetAllClaimsResponse>>
        {
            private readonly ApplicationDbContext _dbContext;

            public Handler(ApplicationDbContext dbContext) => _dbContext = dbContext;

            public async Task<List<GetAllClaimsResponse>> Handle(GetAllClaimsQuery request, CancellationToken cancellationToken)
                => await _dbContext.Claims.Select(q => new GetAllClaimsResponse
                {
                    CustomerName = q.CustomerName,
                    Amount = q.Amount,
                    Description = q.Description,
                    CreatedDate = q.ClaimDate,
                    Status = q.Status.ToString()
                }).ToListAsync(cancellationToken);
        }
    }

    public class GetAllClaimsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("api/claims", async (ISender sender) =>
            {
                var claims = await sender.Send(new GetAllClaims.GetAllClaimsQuery());
                return Results.Ok(claims);
            });
        }
    }
}
