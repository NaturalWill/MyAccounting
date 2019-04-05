
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAccounting.Model
{
    public class Record : BaseDispalyRecord
    {
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RecordId { get; set; }
        public int AccountId { get; set; }
        public virtual Account Account { get; set; }
    }
    public class DispalyRecord : BaseDispalyRecord
    {
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RecordId { get; set; }
        public decimal AssetOffset { get; set; }
        public decimal DebtOffset { get; set; }
        public decimal RealOffset { get { return AssetOffset - DebtOffset; } }

    }
    public class TotalDispalyRecord : BaseDispalyRecord
    {
        public string AccountName { get; set; }

    }
    public class TotalCompareDispalyRecord : BaseDispalyRecord
    {
        public string AccountName { get; set; }
        public DateTime LastRecordDate { get; set; }
        public decimal LastAsset { get; set; }
        public decimal LastDebt { get; set; }
        public decimal LastReal => LastAsset - LastDebt;
        public decimal AssetOffset { get; set; }
        public decimal DebtOffset { get; set; }
        public decimal RealOffset { get { return AssetOffset - DebtOffset; } }

    }
    public class BaseDispalyRecord
    {
        public DateTime RecordDate { get; set; }
        public decimal Asset { get; set; }
        public decimal Debt { get; set; }
        public decimal Real => Asset - Debt;
        public string Info { get; set; }

    }

}
