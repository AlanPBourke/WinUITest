using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinUITest.Data
{
    public class Customer
    {
        public int CustomerId { get; set; }         // pk by convention
        
        [MaxLength(16)]
        public string CustomerCode { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public virtual List<Transaction> Transactions { get; set; }     // virtual so that lazy loading proxies works
        public override string ToString()
        {
            return $"{this.CustomerId}\t{this.CustomerCode}\t{this.Name}\t{this.Balance}";
        }
    }

}
