using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinUITest.Data
{
    public class Product
    {
        public int ProductId { get; set; }
        
        [MaxLength(16)]
        public string ProductCode { get; set; }

        [MaxLength(100)]
        public string ProductName { get; set; } 
        public decimal Price { get; set; }
    }
}
