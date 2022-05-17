namespace WinUITest.Data
{
    public class Customer
    {
        public int CustomerId { get; set; }         // pk by convention

        public string CustomerCode { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public double Balance { get; set; }
        public virtual List<Transaction>? Transactions { get; set; }     // virtual so that lazy loading proxies works
        public override string ToString()
        {
            return $"{this.CustomerId}\t{this.CustomerCode}\t{this.Name}\t{this.Balance}";
        }
    }

}
