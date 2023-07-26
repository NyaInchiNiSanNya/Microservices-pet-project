using AccountManagementMicroservice.DTOs;
using AccountManagementMicroservice.RequestModel;
using AutoMapper;

namespace AccountManagementMicroservice.MappingProfiles
{
    public class TopUpOperationProfile : Profile
    {
        public TopUpOperationProfile()
        {
            SourceMemberNamingConvention = LowerUnderscoreNamingConvention.Instance;
            DestinationMemberNamingConvention = PascalCaseNamingConvention.Instance;

            CreateMap<TopUpOperationDto, PostTopUpRequest>().ReverseMap();
        }
    }
}
