using AccountManagementMicroservice.CQRS.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountManagementMicroservice.Data;
using Microsoft.EntityFrameworkCore;

namespace AccountManagementMicroservice.CQRS.CommandsHandler
{
    public class TopUpOperationCommandHandler : IRequestHandler<TopUpOperationCommand>
    {
        private readonly BankContext _bankContext;

        public TopUpOperationCommandHandler(BankContext bankContext)
        {
            _bankContext = bankContext ?? throw new NullReferenceException(nameof(bankContext));
        }

        public async Task Handle(TopUpOperationCommand request,
            CancellationToken cancellationToken)
        {
            if (String.IsNullOrEmpty(request.accountName) ||
                request.summToTopUp <= 0)
            {
                throw new ArgumentException(nameof(request.summToTopUp));
            }

            var account = await _bankContext.Accounts
                .SingleOrDefaultAsync(x => x.Name.Equals(request.accountName)
                    , cancellationToken: cancellationToken);

            if (account == null)
            {
                throw new NullReferenceException("account does not exist");
            }

            account.Balance += request.summToTopUp;

            await _bankContext.SaveChangesAsync(cancellationToken);
        }
    }
}
