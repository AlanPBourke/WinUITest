using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinUITest.Data
{
    public class TransactionDetail
    {
        public int Quantity { get; set; }
        public decimal Value { get; set; }
        public int TransactionDetailId { get; set; }
        public int TransactionId { get; set; }
        public string ProductCode { get; set; }
        public virtual Transaction? Transaction { get; set; } 

    }
}
