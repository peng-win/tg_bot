using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class OrdersEntity : BaseEntity
    {
        public int OrderId { get; set; }
        public string UserId { get; set; }
        public string UserAddress { get; set; }
        public string UserComment { get; set; }
        public string PaymentType { get; set; }

    }
}
