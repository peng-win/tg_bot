using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class MenuEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
    }
}
