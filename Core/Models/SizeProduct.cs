using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class SizeProduct
    {
        public Guid Id { get; set; } 
        public double Size { get; set; }
        public string Unit { get; set; }
    }
}
