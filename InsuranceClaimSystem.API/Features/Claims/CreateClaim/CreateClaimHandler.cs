using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AutoMapper;
using FluentValidation;
using InsuranceClaimSystem.API.Database;
using InsuranceClaimSystem.API.Domain.ClaimAggregate;
using MediatR;

namespace InsuranceClaimSystem.API.Features.Claims.CreateClaim
{
    public class CreateClaimHandler(ApplicationDbContext context, IMapper mapper, IValidator<CreateClaimRequest> validator) : IRequestHandler<CreateClaimRequest, Result<Guid>>
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<CreateClaimRequest> _validator = validator;

        public async Task<Result<Guid>> Handle(CreateClaimRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return Result<Guid>.Invalid(validationResult.AsErrors());
            }

            var claim = new Claim(request.Name, request.Description, request.Amount, request.CreatedDate, ClaimStatus.Pending);

            await _context.AddAsync(claim, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success(claim.Id);
        }
    }
}