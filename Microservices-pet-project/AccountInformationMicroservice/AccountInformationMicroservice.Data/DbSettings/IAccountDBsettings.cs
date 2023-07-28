using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountInformationMicroservice.Data.DbSettings
{
    public class IAccountDBsettings
    {
        public string AccountsBankCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }

    }
}
