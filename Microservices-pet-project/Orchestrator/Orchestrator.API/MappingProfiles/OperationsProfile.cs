using AutoMapper;
using Orchestrator.API.RequestModels;
using Orchestrator.SharedModels.Request;

namespace Orchestrator.API.MappingProfiles
{
    public class OperationsProfile : Profile
    {
        public OperationsProfile()
        {
            SourceMemberNamingConvention = LowerUnderscoreNamingConvention.Instance;
            DestinationMemberNamingConvention = PascalCaseNamingConvention.Instance;

            CreateMap<ReplenishmentOperationRequest,OperationRequest>().ReverseMap();
            CreateMap<WithdrawalOperationRequest,OperationRequest>().ReverseMap();
        }
    }
}
