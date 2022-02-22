namespace Balance_Transaction.Models
{
    public class Transaction
    {

        public int Id { get; set; }
        public int PersonId { get; set; }
        public DateTime Date { get; set; }
        public string TransactionType { get; set; }
        public double Amount { get; set; }
       

    }
}
