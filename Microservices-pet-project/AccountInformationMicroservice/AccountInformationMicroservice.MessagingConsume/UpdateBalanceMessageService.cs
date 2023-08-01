using AccountInformationMicroservice.IServices;
using AutoMapper;
using FluentValidation;
using MassTransit;
using ShareModel.Requests;
using ShareModel.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountInformationMicroservice.DTOs;

namespace AccountInformationMicroservice.MessagingConsume
{
    public class UpdateBalanceMessageService :
        IConsumer<UpdateAccountBalanceRequest>
    {
        private readonly IValidator<UpdateAccountBalanceRequest> _requestValidator;
        private readonly IAccountInformationManagmentService _accountInformation;
        private readonly IMapper _mapper;

        public UpdateBalanceMessageService(IValidator<UpdateAccountBalanceRequest> requestValidator
            , IAccountInformationManagmentService accountInformation
            , IMapper mapper)
        {
            _requestValidator = requestValidator ??
                                throw new NullReferenceException(nameof(requestValidator));

            _accountInformation = accountInformation ??
                                  throw new NullReferenceException(nameof(accountInformation));

            _mapper = mapper ??
                      throw new NullReferenceException(nameof(mapper));
        }

        public async Task Consume(ConsumeContext<UpdateAccountBalanceRequest> context)
        {

            var operation = context.Message;

            try
            {
                FluentValidation.Results.ValidationResult result = await _requestValidator
                    .ValidateAsync(operation);

                if (!result.IsValid)
                {
                    throw new ArgumentException(nameof(operation));
                }

                await _accountInformation.SetNewAccountBalanceAsync(_mapper.Map<AccountInformationDto>(operation));

                await context.RespondAsync(new BalanceUpdatedResponse
                {
                    AccountName = operation.AccountName,
                });
            }

            catch (NullReferenceException)
            {
                await context.RespondAsync(new BalanceUpdatedResponse
                {
                    AccountName = String.Empty,
                });
            }

        }
    }
}
