using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountInformationMicroservice.CQRS.Commands
{
    public class PutNewAccountBalanceCommand : IRequest
    {
        public String AccountName { get; set; }
        public Decimal NewBalance { get; set; }
    }
}
