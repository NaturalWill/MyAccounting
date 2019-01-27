using System.ComponentModel.DataAnnotations.Schema;

namespace WebMyAccount.Models
{
    public class RecordDetail
    {
        public long RecordDetailId { get; set; }
        public long RecordId { get; set; }
        public int AccountId { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Asset { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Debt { get; set; }
        public decimal Real { get { return Asset - Debt; } }
        public string Info { get; set; }

    }
}
