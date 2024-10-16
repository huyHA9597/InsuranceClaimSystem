using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using AutoMapper;
using FluentValidation;
using InsuranceClaimSystem.API.Database;
using InsuranceClaimSystem.API.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InsuranceClaimSystem.API.Features.Claims.UpdateClaimStatus
{
    public class UpdateClaimStatusHandler(ApplicationDbContext context, IMapper mapper, IValidator<UpdateClaimStatusRequest> validator) : IRequestHandler<UpdateClaimStatusRequest, Result<string>>
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly IValidator<UpdateClaimStatusRequest> _validator = validator;

        public async Task<Result<string>> Handle(UpdateClaimStatusRequest request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return Result.Invalid(validationResult.AsErrors());
            }

            var searchResult = _context.Claims.AsNoTracking().Where(c => c.CustomerName == request.Name && c.Amount == request.Amount && c.ClaimDate == request.CreatedDate).FirstOrDefaultAsync().Result;

            if (searchResult == null)
            {
                return Result.NotFound("No claim record found!");
            }
            else
            {
                searchResult.Status = RandomClaimProcessExtension.ToProcessClaim();

                _context.Update(searchResult);

                await _context.SaveChangesAsync(cancellationToken);

                return Result.Success($"Claim update with status {searchResult.Status}");
            }
        }
    }
}
