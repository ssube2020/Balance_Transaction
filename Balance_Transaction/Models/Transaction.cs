namespace Balance_Transaction.Models
{
    public class Transaction
    {

        public int Id { get; set; }
        public int PersonId { get; set; }
        public DateTime Date { get; set; }

        public Transaction()
        {

        }

        public Transaction(int id, int personid, DateTime date) 
        {
            Id = id;
            PersonId = personid;
            Date = date;
        }

    }
}
