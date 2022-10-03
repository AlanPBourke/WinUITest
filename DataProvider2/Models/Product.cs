﻿namespace WinUITest.Data;
public class Product
{
    public int ProductId { get; set; }
    public string ProductCode { get; set; } = String.Empty;
    public string ProductName { get; set; } = String.Empty;
    public double Price { get; set; }
    public int TransactionDetailId { get; set; }

}
