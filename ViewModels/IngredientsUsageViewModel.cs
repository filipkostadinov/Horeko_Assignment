using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels
{
    public class IngredientsUsageViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double TotalAmount { get; set; }
        public int NumberOfDishes { get; set; }
    }
}
