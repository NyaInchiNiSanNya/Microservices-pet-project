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
        private readonly IAccountInformationMicroserviceMessaging _accountInformation;
        private readonly IMapper _mapper;

        public BankController(IBankManagementMicroserviceMessaging bankManagement,
            IAccountInformationMicroserviceMessaging accountInformation,
            IMapper mapper)
        {
            _bankManagement=bankManagement
                            ?? throw new NullReferenceException(nameof(bankManagement));

            _mapper = mapper
                      ?? throw new NullReferenceException(nameof(mapper));

            _accountInformation = accountInformation
                                  ?? throw new NullReferenceException(nameof(accountInformation));
        }

        [HttpPost]
        public async Task<IActionResult> Get([FromBody] OperationRequest operationRequest)
        {

            try
            {
                Decimal newBalance=12;

                if (operationRequest.Withdrawal)
                {
                    newBalance = await _bankManagement.SendWithdrawalOperationMessage(
                        _mapper.Map<WithdrawalOperationRequest>(operationRequest));
                }

                if (operationRequest.Replenishment)
                {
                    newBalance = await _bankManagement.SendReplenishmentOperationMessage(
                        _mapper.Map<ReplenishmentOperationRequest>(operationRequest));
                }

                await _accountInformation.SendNewBalanceMessage(new UpdateAccountBalanceRequest()
                {
                    AccountName = operationRequest.AccountName,
                    newBalance = newBalance
                });

            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
            catch (InvalidOperationException)
            {
                return BadRequest("\r\ninsufficient funds");
            }

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAccountBalanceRequest operationRequest)
        {
            
            try
            {
                return Ok(await _accountInformation.GetBalanceMessage(new GetAccountBalanceRequest()
                {
                    AccountName = operationRequest.AccountName,
                }));

            }
            catch (NullReferenceException)
            {

                return NotFound();

            }
        }
    }
}