using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Orchestrator.API.RequestModels;
using Orchestrator.IService;
using Orchestrator.SharedModels.Request;

namespace Orchestrator.Controllers
{
    [ApiController]
    [Route("Bank")]
    public class BankController : ControllerBase
    {

        private readonly IBankManagementMicroserviceMessaging _bankManagement;
        private readonly IMapper _mapper;

        public BankController(IBankManagementMicroserviceMessaging bankManagement,
            IMapper mapper)
        {
            _bankManagement=bankManagement
                            ?? throw new NullReferenceException(nameof(bankManagement));

            _mapper = mapper
                      ?? throw new NullReferenceException(nameof(mapper));
        }

        [HttpPost]
        public async Task<IActionResult> Get([FromBody] OperationRequest operationRequest)
        {
            if (operationRequest.Withdrawal)
            {
               await _bankManagement.SendWithdrawalOperationMessage(
                    _mapper.Map<WithdrawalOperationRequest>(operationRequest));
            }

            return Ok();
        }
    }
}