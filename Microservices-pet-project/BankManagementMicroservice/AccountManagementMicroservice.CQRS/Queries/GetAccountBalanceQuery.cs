using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagementMicroservice.CQRS.Queries
{
    public class GetAccountBalanceQuery : IRequest<Decimal>
    {
        public String accountName { get; set; }
    }
}
