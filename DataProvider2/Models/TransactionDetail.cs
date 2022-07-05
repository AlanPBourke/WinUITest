namespace WinUITest.Data;
public class TransactionDetail
{
    public int Quantity { get; set; }
    public double Price { get; set; }
    public double Value { get; set; }
    public int TransactionDetailId { get; set; }
    public int TransactionId { get; set; }
    public int ProductId { get; set; }
    public virtual Product? Product { get; set; }
    public virtual Transaction? Transaction { get; set; }

}
