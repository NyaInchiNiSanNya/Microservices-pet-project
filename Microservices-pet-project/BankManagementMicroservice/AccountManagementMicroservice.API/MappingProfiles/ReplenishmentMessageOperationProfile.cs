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

            CreateMap<TopUpOperationDto, ReplenishmentOperationRequest>().ReverseMap();
        }
    }
}
