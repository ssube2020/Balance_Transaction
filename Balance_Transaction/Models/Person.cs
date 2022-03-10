using System.ComponentModel.DataAnnotations;

namespace Balance_Transaction.Models
{
    public class Person 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        [Required]
        public double Balance { get; set; }
        public DateTime Date { get; set; }
        public string PrivateNo { get; set; }

        public Person()
        {

        }

        public Person(string name, string surname, double balance, DateTime dt)
        {
            this.Name = name;
            this.Surname = surname;
            this.Balance = balance;
            this.Date = dt;
        }

        public void AddToBalance(int amount)
        {
            Balance += amount;
        }

        public double GetBalance()
        {
            return Balance;
        }

        public void ReduceBalance(double amount)
        {
            Balance -= amount;    
        }

        public void SetBalance(double balance)
        {
            Balance = balance;
        }
    }
}
