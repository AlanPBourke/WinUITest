namespace WinUITest.Data;

// Does not create a db table, is instead mapped to a db view in the context.
// the view is defined manually in the initial migration.
public class CustomerTransaction
{
    public int TransactionId { get; set; }
    public DateTime TransactionDate { get; set; }
    public string Type { get; set; }
    public double Value { get; set; }
    public string CustomerCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;

}
