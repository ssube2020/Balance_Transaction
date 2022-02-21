namespace Balance_Transaction.Models
{
    public interface IPerson
    {
        public string Name { get; set; }
        public string SurnName { get; set; }
        public double Balance { get; set; }
        public void AddToBalance(int amount);
        public double GetBalance();
        public void SetBalance(double balance);
        public void  ReduceBalance(double amount);


    }
}
