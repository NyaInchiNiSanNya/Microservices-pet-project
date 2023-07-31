using AccountManagementMicroservice.DTOs;
using AutoMapper;
using ShareModel.Requests;

namespace AccountManagementMicroservice.MappingProfiles
{
    public class WithdrawalMessageOperationProfile:Profile
    {
        public WithdrawalMessageOperationProfile()
        {
            SourceMemberNamingConvention = LowerUnderscoreNamingConvention.Instance;
            DestinationMemberNamingConvention = PascalCaseNamingConvention.Instance;

            CreateMap<WithdrawalOperationRequest, WithdrawalOperationDto>()
                .ForMember(
                    dest => dest.WithdrawalAmount,
                    opt => opt.MapFrom(src => src.Amount)
                );

            CreateMap<WithdrawalOperationDto, WithdrawalOperationRequest>()
                .ForMember(
                    dest => dest.Amount,
                    opt => opt.MapFrom(src => src.WithdrawalAmount)
                );
        }
    }
}
