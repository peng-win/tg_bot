using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class PaymentTypeEntity : BaseEntity
    {
        public int TypeId { get; set; }
        public string TypeName { get; set; }
    }
}
