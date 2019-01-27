using System.Collections.Generic;

namespace WebMyAccount.Models
{
    public class Record
    {
        public System.DateTime RecordDate { get; set; }
        public string Info { get; set; }
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RecordId { get; set; }
    }
    public class RecordDto : Record
    {
        public virtual List<RecordDetail> Details { get; set; }

    }
}
