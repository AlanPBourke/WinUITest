using System.ComponentModel.DataAnnotations;

namespace WinUITest.Data
{
    public class Product
    {
        public int ProductId { get; set; }

        [MaxLength(16)]
        public string? ProductCode { get; set; }

        [MaxLength(100)]
        public string? ProductName { get; set; }
        public decimal Price { get; set; }
    }
}
