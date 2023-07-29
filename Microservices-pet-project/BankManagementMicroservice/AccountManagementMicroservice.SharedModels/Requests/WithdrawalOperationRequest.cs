using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagementMicroservice.SharedModels.Requests
{
    public class WithdrawalOperationRequest
    {
        public String AccountName { get; set; }
        public Decimal Amount { get; set; }
    }
}
