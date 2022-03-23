using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Ingridient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double WeightInGrams { get; set; }  
        public decimal Price { get; set; }
    }
}
