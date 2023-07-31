using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Orchestrator.API.RequestModels;
using Orchestrator.IService;
using ShareModel.Requests;

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

            try
            {
                if (operationRequest.Withdrawal)
                {
                    var newBalance = await _bankManagement.SendWithdrawalOperationMessage(
                        _mapper.Map<WithdrawalOperationRequest>(operationRequest));
                }

                if (operationRequest.Replenishment)
                {
                    var newBalance = await _bankManagement.SendReplenishmentOperationMessage(
                        _mapper.Map<ReplenishmentOperationRequest>(operationRequest));
                }



            }
            catch (NullReferenceException)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}