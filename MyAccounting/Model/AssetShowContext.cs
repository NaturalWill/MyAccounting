using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAccounting.Model
{
    public class AssetShowContext : DbContext
    {

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Record> Records { get; set; }


    }
}
