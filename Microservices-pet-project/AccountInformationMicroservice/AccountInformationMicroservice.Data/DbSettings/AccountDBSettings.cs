using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountInformationMicroservice.Data.DbSettings
{
    public class AccountDBSettings : IAccountDBsettings
    {
        public string AccountsBankCollectionName { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
    }
}
