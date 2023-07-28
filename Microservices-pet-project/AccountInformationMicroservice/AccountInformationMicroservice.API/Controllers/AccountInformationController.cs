using System.ComponentModel.DataAnnotations;
using AccountInformationMicroservice.API.FluentValidation;
using AccountInformationMicroservice.API.RequestModels;
using AccountInformationMicroservice.DTOs;
using AccountInformationMicroservice.IServices;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace AccountInformationMicroservice.Controllers
{
    [ApiController]
    [Route("AccountInformation")]
    public class AccountInformationController : ControllerBase
    {
        private readonly IValidator<GetAccountBalanceRequest> _getAccountBalanceRequestValidator;
        private readonly IValidator<PutNewAccountBalanceRequest> _postNewAccountBalanceRequestValidator;
        private readonly IAccountInformationManagmentService _accountInformationManagmentService;
        private readonly IMapper _mapper;

        public AccountInformationController(IValidator<GetAccountBalanceRequest> getAccountBalanceRequestValidator, 
            IAccountInformationManagmentService accountInformationManagmentService,
            IValidator<PutNewAccountBalanceRequest> postNewAccountBalanceRequestValidator,
            IMapper mapper)
        {
            _getAccountBalanceRequestValidator = getAccountBalanceRequestValidator 
                                                 ?? throw new NullReferenceException(
                                                     nameof(getAccountBalanceRequestValidator));
            
            _accountInformationManagmentService = accountInformationManagmentService
                                                  ?? throw new NullReferenceException(
                                                      nameof(accountInformationManagmentService));

            _postNewAccountBalanceRequestValidator = postNewAccountBalanceRequestValidator
                                                     ?? throw new NullReferenceException(
                                                         nameof(postNewAccountBalanceRequestValidator));

            _mapper = mapper
                      ?? throw new NullReferenceException(
                          nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAccountBalanceRequest accountInformation)
        {
            ValidationResult result = await _getAccountBalanceRequestValidator
                .ValidateAsync(accountInformation);
            
            if (!result.IsValid)
            {
                return BadRequest();
            }

            try
            {
                return Ok(await _accountInformationManagmentService
                    .GetAccountBalanceByAccountNameAsync(accountInformation.AccountName));
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }
        }

            [HttpPut]
        public async Task<IActionResult> Post([FromBody] PutNewAccountBalanceRequest accountInformation)
        {
            ValidationResult result = await _postNewAccountBalanceRequestValidator
                .ValidateAsync(accountInformation);

            if (!result.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _accountInformationManagmentService
                    .SetNewAccountBalanceAsync(_mapper.Map<AccountInformationDto>(accountInformation));
            }
            catch (NullReferenceException)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}