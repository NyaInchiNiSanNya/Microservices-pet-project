using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountManagementMicroservice.CQRS.Queries;
using AccountManagementMicroservice.Data;
using Microsoft.EntityFrameworkCore;

namespace AccountManagementMicroservice.CQRS.QueriesHandler
{
    internal class GetAccountBalanceQueryHandler : IRequestHandler<GetAccountBalanceQuery, Decimal>
    {
        private readonly BankContext _bankContext;

        public GetAccountBalanceQueryHandler(BankContext bankContext)
        {
            _bankContext = bankContext ?? throw new NullReferenceException(nameof(bankContext));
        }

        public async Task<Decimal> Handle(GetAccountBalanceQuery request, CancellationToken cancellationToken)
        {
            if (String.IsNullOrEmpty(request.accountName))
            {
                throw new ArgumentException(nameof(request));
            }

            return await _bankContext.Accounts.Where(x=>x.Name.Equals(request.accountName)).Select(x=>x.Balance)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }
    }
}
