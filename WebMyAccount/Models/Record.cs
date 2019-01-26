using System.Collections.Generic;

namespace WebMyAccount.Models
{
    public class Record
    {
        public System.DateTime RecordDate { get; set; }
        public string Info { get; set; }
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RecordId { get; set; }
        public virtual List<RecordDetail> Details { get; set; }
    }
}
