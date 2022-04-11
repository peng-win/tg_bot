﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Menu
    {
        public Guid Id { get; set; }
        public string Product { get; set; }
        public double Size { get; set; }
        public double WeightInGrams { get; set; }
        public decimal Price { get; set; }
    }
}
