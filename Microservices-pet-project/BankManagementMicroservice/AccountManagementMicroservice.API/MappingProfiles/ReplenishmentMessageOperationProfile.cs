using AccountManagementMicroservice.DTOs;
using AutoMapper;
using ShareModel.Requests;

namespace AccountManagementMicroservice.MappingProfiles
{
    public class TopUpMessageOperationProfile : Profile
    {
        public TopUpMessageOperationProfile()
        {
            SourceMemberNamingConvention = LowerUnderscoreNamingConvention.Instance;
            DestinationMemberNamingConvention = PascalCaseNamingConvention.Instance;

            CreateMap<TopUpOperationDto, ReplenishmentOperationRequest>().ReverseMap().ForMember(
                dest => dest.TopUpAmount,
                opt => opt.MapFrom(src => src.Amount)
            );
        }
    }
}
