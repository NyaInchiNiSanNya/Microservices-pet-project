using AccountManagementMicroservice.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagementMicroservice.IServices
{
    public interface ITopUpOperationService
    {
        public Task TopUpMoneyToAccount(TopUpOperationDto topUpOperation);
    }
}
