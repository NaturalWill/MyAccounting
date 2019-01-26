using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMyAccount.Models
{
    public class MyAccountContext : DbContext
    {
        public MyAccountContext(DbContextOptions<MyAccountContext> options)
             : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Record> Records { get; set; }
        public DbSet<RecordDetail> RecordDetails { get; set; }
    }
}
