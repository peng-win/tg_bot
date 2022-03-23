using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public string Product1 { get; set; }
        public string Product2 { get; set; }
        public string Product3 { get; set; }
        public string Product4 { get; set; }
        public string Product5 { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public DateTime DateOfCreation { get; set; }    
        public string Description { get; set; }
        public string PaymentType { get; set; }
        public string Status { get; set; }
    }
}
