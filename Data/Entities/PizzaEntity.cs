using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class PizzaEntity : BaseEntity
    {
        public int PizzaId { get; set; }
        public string PizzaName { get; set; }
    }
}
