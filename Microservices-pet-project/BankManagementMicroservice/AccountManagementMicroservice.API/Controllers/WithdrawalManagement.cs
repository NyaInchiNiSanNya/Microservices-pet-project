using AccountManagementMicroservice.DTOs;
using AccountManagementMicroservice.IServices;
using AccountManagementMicroservice.RequestModel;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankManagementMicroservice.Controllers
{
    [ApiController]
    [Route("WithdrawalManagement")]
    public class WithdrawalManagement : ControllerBase
    {
        
        private readonly IValidator<PostWithdrawalRequest> _operationValidator;
        private readonly IWithdrawOperationService _withdrawOperation;
        private readonly IMapper _mapper;

        public WithdrawalManagement(IValidator<PostWithdrawalRequest> operationValidator
            , IWithdrawOperationService withdrawOperation
            ,IMapper mapper)
        {
            _operationValidator = operationValidator ?? 
                                  throw new NullReferenceException(nameof(operationValidator));
            _withdrawOperation = withdrawOperation ?? 
                                 throw new NullReferenceException(nameof(withdrawOperation));
            _mapper = mapper ?? 
                      throw new NullReferenceException(nameof(mapper));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostWithdrawalRequest request)
        {
            ValidationResult result = await _operationValidator
            .ValidateAsync(request);

            if (!result.IsValid)
            {
                return BadRequest();
            }

            await _withdrawOperation.WithdrawMoneyFromAccount(_mapper.Map<WithdrawalOperationDto>(request));

            return Ok();
        }
    }
}