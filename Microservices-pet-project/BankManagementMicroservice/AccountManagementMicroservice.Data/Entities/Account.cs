﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagementMicroservice.Data.Entities
{
    public class Account
    {
        public Int32 Id { get; set; }
        public Decimal Balance { get; set; }
        public String Name { get; set; }
    }
}
