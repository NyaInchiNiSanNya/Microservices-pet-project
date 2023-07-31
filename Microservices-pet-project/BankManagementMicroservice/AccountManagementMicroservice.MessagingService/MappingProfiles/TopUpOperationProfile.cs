using AccountManagementMicroservice.DTOs;
using AutoMapper;
using ShareModel.Requests;

namespace AccountManagementMicroservice.MappingProfiles
{
    public class TopUpOperationProfile : Profile
    {
        public TopUpOperationProfile()
        {
            SourceMemberNamingConvention = LowerUnderscoreNamingConvention.Instance;
            DestinationMemberNamingConvention = PascalCaseNamingConvention.Instance;

            CreateMap<TopUpOperationDto, ReplenishmentOperationRequest>().ReverseMap();
        }
    }
}
