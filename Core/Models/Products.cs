using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Products
    {
        public Guid Id { get; set; }
        public string Product { get; set; }
        public string TypeProduct { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
    }
}
