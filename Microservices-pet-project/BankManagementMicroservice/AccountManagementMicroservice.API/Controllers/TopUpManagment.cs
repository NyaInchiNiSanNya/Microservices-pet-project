using AccountManagementMicroservice.DTOs;
using AccountManagementMicroservice.IServices;
using AccountManagementMicroservice.RequestModel;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccountManagementMicroservice.Controllers
{
    [ApiController]
    [Route("TopUpManagement")]
    public class WithdrawalManagement : ControllerBase
    {

        private readonly IValidator<PostTopUpRequest> _operationValidator;
        private readonly ITopUpOperationService _topUpOperation;
        private readonly IMapper _mapper;

        public WithdrawalManagement(IValidator<PostTopUpRequest> operationValidator
            , ITopUpOperationService topUpOperation
            , IMapper mapper)
        {
            _operationValidator = operationValidator ??
                                  throw new NullReferenceException(nameof(operationValidator));
            _topUpOperation = topUpOperation ??
                              throw new NullReferenceException(nameof(topUpOperation));
            _mapper = mapper ??
                      throw new NullReferenceException(nameof(mapper));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostTopUpRequest request)
        {
            ValidationResult result = await _operationValidator
                .ValidateAsync(request);

            if (!result.IsValid)
            {
                return BadRequest();
            }

            await _topUpOperation.TopUpMoneyToAccount(_mapper.Map<TopUpOperationDto>(request));

            return Ok();
        }
    }
}
