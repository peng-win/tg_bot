using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Menu
    {
        public int MenuId { get; set; }
        public string Name { get; set; }
        public string Size_Small { get; set; }
        public decimal Price_Small { get; set; }
        public string Size_Medium { get; set; }
        public decimal Price_Medium { get; set; }
        public string Size_Large { get; set; }
        public decimal Price_Large { get; set; }
        public string Description { get; set; }
    }
}
