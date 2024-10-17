using AutoMapper;
using InsuranceClaimSystem.API.Domain;

namespace InsuranceClaimSystem.API.Features.Claims
{
    public class ClaimMapperProfile : Profile
    {
        public ClaimMapperProfile()
        {
            CreateMap<Claim, ClaimResponse>()
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.ClaimDate))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.CustomerName))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        }

        public override string ProfileName => nameof(ClaimMapperProfile);
    }
}
