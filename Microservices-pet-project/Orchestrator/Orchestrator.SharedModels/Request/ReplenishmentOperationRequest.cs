using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchestrator.SharedModels.Request
{
    public class ReplenishmentOperationRequest
    {
        public String AccountName { get; set; }
        public Decimal Amount { get; set; }
    }
}
