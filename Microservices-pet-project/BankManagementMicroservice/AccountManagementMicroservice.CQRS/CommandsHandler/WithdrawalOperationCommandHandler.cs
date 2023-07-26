using MediatR;
using AccountManagementMicroservice.CQRS.Commands;
using AccountManagementMicroservice.Data;
using Microsoft.EntityFrameworkCore;

namespace AccountManagementMicroservice.CQRS.CommandsHandler
{
    public class WithdrawalOperationCommandHandler : IRequestHandler<WithdrawalOperationCommand>
    {
        private readonly BankContext _bankContext;

        public WithdrawalOperationCommandHandler(BankContext bankContext)
        {
            _bankContext = bankContext ?? throw new NullReferenceException(nameof(bankContext));
        }

        public async Task Handle(WithdrawalOperationCommand request,
            CancellationToken cancellationToken)
        {
           if(String.IsNullOrEmpty(request.accountName) || 
              request.summToWithdrawal<=0)
           {
               throw new ArgumentException(nameof(request.summToWithdrawal));
           }

           var account = await _bankContext.Accounts
               .SingleOrDefaultAsync(x => x.Name.Equals(request.accountName)
                   , cancellationToken: cancellationToken);

           if (account == null)
           {
               throw new NullReferenceException("account does not exist");
           }

           if (account.Balance-request.summToWithdrawal>=0)
           {
               account.Balance -= request.summToWithdrawal;

               await _bankContext.SaveChangesAsync(cancellationToken);
               return;
           }

           throw new InvalidOperationException("insufficient funds");
        }
    }
}
