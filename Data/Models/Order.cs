using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string PaymentType { get; set; }
    }
}
