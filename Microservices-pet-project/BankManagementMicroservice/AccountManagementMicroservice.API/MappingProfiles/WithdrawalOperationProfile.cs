using AccountManagementMicroservice.DTOs;
using AccountManagementMicroservice.RequestModel;
using AutoMapper;

namespace AccountManagementMicroservice.MappingProfiles
{
    public class WithdrawalOperationProfile:Profile
    {
        public WithdrawalOperationProfile()
        {
            SourceMemberNamingConvention = LowerUnderscoreNamingConvention.Instance;
            DestinationMemberNamingConvention = PascalCaseNamingConvention.Instance;

            CreateMap<WithdrawalOperationDto, PostWithdrawalRequest>().ReverseMap();
        }
    }
}
