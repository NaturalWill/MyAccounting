namespace WebMyAccount.Models
{
    public class RecordDetail : BaseDispalyRecord
    {
        public long RecordDetailId { get; set; }
        public long RecordId { get; set; }
        public int AccountId { get; set; }
        
        public virtual Account Account { get; set; }
    }
    public class BaseDispalyRecord
    {
        public decimal Asset { get; set; }
        public decimal Debt { get; set; }
        public decimal Real { get { return Asset - Debt; } }
        public string Info { get; set; }

    }
}
