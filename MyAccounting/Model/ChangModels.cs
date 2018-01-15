using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAccounting.Model
{
    public class Change
    {

        //  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccountId { get; set; }
        public string Info { get; set; }
        public int ChangeTypeId { get; set; }
    }

    public class ChangeType
    {
        public int ChangeTypeId { get; set; }

        public string Name { get; set; }
    }
}
