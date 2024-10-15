using Carter;
using InsuranceClaimSystem.API.Database;
using InsuranceClaimSystem.API.Domain;
using MediatR;
using static InsuranceClaimSystem.API.Features.Claims.CreateClaim;

namespace InsuranceClaimSystem.API.Features.Claims
{
    public static class CreateClaim
    {
        public sealed class CreateClaimCommand : IRequest<Guid>
        {
            public string CustomerName { get; set; } = string.Empty;
            public float Amount { get; set; }
            public string Description { get; set; } = string.Empty;
            public DateTime Date { get; set; }
            public ClaimStatus Status { get; set; } = ClaimStatus.Pending;
        }

        internal sealed class Handler : IRequestHandler<CreateClaimCommand, Guid>
        {
            private readonly ApplicationDbContext _dbContext;

            public Handler(ApplicationDbContext dbContext) => _dbContext = dbContext;

            public async Task<Guid> Handle(CreateClaimCommand request, CancellationToken cancellationToken)
            {
                var claim = new Claim
                {
                    Id = Guid.NewGuid(),
                    CustomerName = request.CustomerName,
                    Amount = request.Amount,
                    Description = request.Description,
                    ClaimDate = request.Date,
                    Status = request.Status,
                };
                await _dbContext.AddAsync(claim, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return claim.Id;
            }
        }
    }

    public class CreateClaimEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("api/claim", async (CreateClaimCommand request, ISender sender) =>
            {
                var claimId = await sender.Send(request);
                return Results.Ok(claimId);
            });
        }
    }
}
