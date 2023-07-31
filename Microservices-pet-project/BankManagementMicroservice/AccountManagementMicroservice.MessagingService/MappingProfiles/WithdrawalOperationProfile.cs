using AccountManagementMicroservice.DTOs;
using AutoMapper;
using ShareModel.Requests;

namespace AccountManagementMicroservice.MappingProfiles
{
    public class WithdrawalOperationProfile:Profile
    {
        public WithdrawalOperationProfile()
        {
            SourceMemberNamingConvention = LowerUnderscoreNamingConvention.Instance;
            DestinationMemberNamingConvention = PascalCaseNamingConvention.Instance;

            CreateMap<WithdrawalOperationDto, WithdrawalOperationRequest>().ReverseMap();
        }
    }
}
