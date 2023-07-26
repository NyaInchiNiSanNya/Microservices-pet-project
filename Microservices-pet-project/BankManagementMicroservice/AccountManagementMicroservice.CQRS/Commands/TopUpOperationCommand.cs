using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccountManagementMicroservice.DTOs;
using MediatR;

namespace AccountManagementMicroservice.CQRS.Commands
{
    public class TopUpOperationCommand : IRequest
    {
        public String accountName { get; set; }
        public Decimal summToTopUp { get; set; }
    }
}
