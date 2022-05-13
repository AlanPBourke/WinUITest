namespace WinUITest.Data
{
    public class CustomerTransaction
    {
        public int TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Type { get; set; }
        public Decimal Value { get; set; }
        public string CustomerCode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

    }
}
