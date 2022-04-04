using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string ProductIds { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public DateTime DateOfCreation { get; set; }    
        public DateTime DateOfCompletion { get; set; }    
        public string Description { get; set; }
        public string PaymentType { get; set; }
        public string Status { get; set; }
    }
}
