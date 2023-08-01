using MassTransit;
using Orchestrator.IService;
using ShareModel.Requests;
using ShareModel.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchestrator.MessagingService
{
    public class AccountInformationMicroserviceMessaging: IAccountInformationMicroserviceMessaging
    {
        IRequestClient<GetAccountBalanceRequest> _clientGetBalance;
        IRequestClient<UpdateAccountBalanceRequest> _clientUpdateBalance;

        public AccountInformationMicroserviceMessaging(IRequestClient<UpdateAccountBalanceRequest> clientUpdateBalance
            , IRequestClient<GetAccountBalanceRequest> clientGetBalance)
        {
            _clientGetBalance = clientGetBalance
                                ?? throw new NullReferenceException(nameof(clientGetBalance));

            _clientUpdateBalance = clientUpdateBalance
                                   ?? throw new NullReferenceException(nameof(clientUpdateBalance));
        }

        public async Task SendNewBalanceMessage(UpdateAccountBalanceRequest request)
        {
            var response = await _clientUpdateBalance.GetResponse<BalanceUpdatedResponse>(new
            {
                request.AccountName,
                request.newBalance
            });


            if (String.IsNullOrEmpty(response.Message.AccountName))
            {
                throw new NullReferenceException(nameof(UpdateAccountBalanceRequest));
            }
        }

        public async Task<Decimal> GetBalanceMessage(GetAccountBalanceRequest request)
        {
            var response = await _clientGetBalance.GetResponse<AccountInformationResponse>(new
            {
                request.AccountName
            });


            if (String.IsNullOrEmpty(response.Message.AccountName))
            {
                throw new NullReferenceException(nameof(UpdateAccountBalanceRequest));
            }

            return response.Message.AccountBalance;
        }
    }
}
