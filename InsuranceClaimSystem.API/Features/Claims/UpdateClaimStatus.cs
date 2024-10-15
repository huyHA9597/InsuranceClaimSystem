using Carter;
using InsuranceClaimSystem.API.Database;
using InsuranceClaimSystem.API.Domain;
using InsuranceClaimSystem.API.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InsuranceClaimSystem.API.Features.Claims
{
    public static class UpdateClaimStatus
    {
        public sealed class Command : IRequest<Guid>
        {
            public Guid ClaimId { get; set; }
        }

        internal sealed class Handler : IRequestHandler<Command, Guid>
        {
            private readonly ApplicationDbContext _dbContext;

            public Handler(ApplicationDbContext dbContext) => _dbContext = dbContext;

            public async Task<Guid> Handle(Command request, CancellationToken cancellationToken)
            {
                var claim = await _dbContext.Claims
                    .AsNoTracking()
                    .Where(s => s.Id == request.ClaimId)
                    .FirstOrDefaultAsync();

                if (claim == null)
                {
                    throw new InvalidDataException(request.ClaimId.ToString());
                }

                claim = new Claim
                {
                    Id = claim.Id,
                    Status = RandomClaimProcessExtension.ToProcessClaim(),
                    Amount = claim.Amount,
                    ClaimDate = claim.ClaimDate,
                    CustomerName = claim.CustomerName,
                    Description = claim.Description,
                };

                _dbContext.Update(claim);
                await _dbContext.SaveChangesAsync(cancellationToken);
                return claim.Id;
            }
        }

        public class UpdateClaimStatusEndpoint : ICarterModule
        {
            public void AddRoutes(IEndpointRouteBuilder app)
            {
                app.MapPut("api/claim", async (Guid id, ISender sender) =>
                {
                    var command = new UpdateClaimStatus.Command { ClaimId = id };

                    try
                    {
                        var result = await sender.Send(command);
                        return Results.Ok(result);
                    }
                    catch (Exception)
                    {
                        return Results.NotFound();
                    }
                });
            }
        }
    }
}
