using FluentValidation;

namespace InsuranceClaimSystem.API.Features.Claims.UpdateClaimStatus
{
    public class UpdateClaimRequestValidator : AbstractValidator<UpdateClaimStatusRequest>
    {
        public UpdateClaimRequestValidator()
        {
            RuleFor(request => request.Name)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(request => request.Amount)
                .NotEmpty()
                .GreaterThanOrEqualTo(0);
        }
    }
}
