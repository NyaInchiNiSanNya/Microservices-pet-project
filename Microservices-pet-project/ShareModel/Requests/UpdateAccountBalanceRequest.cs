using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareModel.Requests
{
    public class UpdateAccountBalanceRequest
    {
        public String AccountName { get; set; }
        public Decimal newBalance { get; set; }
    }
}
