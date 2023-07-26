using System.Collections.Generic;
using System.Xml.Linq;
using AccountManagementMicroservice.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AccountManagementMicroservice.Data
{
    public class BankContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }

        public BankContext(DbContextOptions<BankContext> options) : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            base.OnConfiguring(optionBuilder);
        }
    }
}