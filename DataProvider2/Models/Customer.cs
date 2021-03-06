using System.ComponentModel.DataAnnotations;

namespace WinUITest.Data
{
    public class Customer
    {
        public int CustomerId { get; set; }         // pk by convention

        [Required]
        [MinLength(1)]
        [MaxLength(16)]
        public string CustomerCode { get; set; } = string.Empty;

        [Required]
        [MinLength(1)]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public virtual List<Transaction>? Transactions { get; set; }     // virtual so that lazy loading proxies works
        public override string ToString()
        {
            return $"{this.CustomerId}\t{this.CustomerCode}\t{this.Name}\t{this.Balance}";
        }
    }

}
