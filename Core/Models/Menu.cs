using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Menu
    {
        public Guid Id { get; set; }

        public string Product { get; set; }
        //public string Size { get; set; }

        public string TypeProduct { get; set; }
        public double WeightInGrams { get; set; }
        public decimal Price { get; set; }
    }
}
