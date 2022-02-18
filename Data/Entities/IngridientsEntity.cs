using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class IngridientsEntity : BaseEntity
    {
        public int IngridientId { get; set; }
        public string IngridientName { get; set; }
    }
}
