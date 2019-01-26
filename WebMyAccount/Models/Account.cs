namespace WebMyAccount.Models
{
    public class Account
    {

        //  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AccountId { get; set; }
        public string Name { get; set; }
        public int Ratio { get; set; }

    }
}
