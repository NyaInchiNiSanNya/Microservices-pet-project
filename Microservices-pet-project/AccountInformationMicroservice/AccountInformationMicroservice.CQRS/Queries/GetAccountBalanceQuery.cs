using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace AccountInformationMicroservice.CQRS.Queries
{
    public class GetAccountBalanceQuery : IRequest<decimal>
    {
        public String AccountName { get; set; }
    }
}
