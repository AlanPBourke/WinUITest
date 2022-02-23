using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinUITest.Data
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        
        [MaxLength(1)]
        public string Type { get; set; }
        public Decimal Value { get; set; }
        public int CustomerId { get; set; }     // FK by convention
        public DateTime TransactionDate { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual List<TransactionDetail> TransactionDetails { get; set; }

    }
}
