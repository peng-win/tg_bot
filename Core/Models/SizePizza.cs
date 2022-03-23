using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class SizePizza
    {
        public int Id { get; set; } 
        public string Size { get; set; }
        public double Diameter { get; set; }
        public string Unit { get; set; }
    }
}
