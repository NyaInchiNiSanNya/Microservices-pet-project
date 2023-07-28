using AccountInformationMicroservice.API.RequestModels;
using AccountInformationMicroservice.DTOs;
using AutoMapper;

namespace AccountInformationMicroservice.API.Profiles
{
    public class AccountInformation : Profile
    {
        public AccountInformation()
        {
            SourceMemberNamingConvention = LowerUnderscoreNamingConvention.Instance;
            DestinationMemberNamingConvention = PascalCaseNamingConvention.Instance;

            CreateMap<AccountInformationDto,PutNewAccountBalanceRequest>().ReverseMap()
                .ForMember(
                    dest => dest.AccountBalance,
                    opt =>
                        opt.MapFrom(src => src.NewAccountBalance)
                );
        }
    }

}
