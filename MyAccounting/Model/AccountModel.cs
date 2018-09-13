using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAccounting.Model
{
    public class Account
    {

        //  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccountId { get; set; }
        public string Name { get; set; }
        public int Ratio { get; set; }

        public virtual List<Record> Records { get; set; }
    }
}
