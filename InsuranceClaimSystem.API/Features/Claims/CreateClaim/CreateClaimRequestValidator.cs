using FluentValidation;

namespace InsuranceClaimSystem.API.Features.Claims.CreateClaim
{
    public class CreateClaimRequestValidator : AbstractValidator<CreateClaimRequest>
    {
        // Add rule to validate the claim adding model request
        public CreateClaimRequestValidator()
        {
            RuleFor(request => request.Name)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(request => request.Description)
                .NotEmpty()
                .MaximumLength(300);

            RuleFor(request => request.Amount)
                .NotEmpty()
                .GreaterThanOrEqualTo(0);
        }
    }
}
