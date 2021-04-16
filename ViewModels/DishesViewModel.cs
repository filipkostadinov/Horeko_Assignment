using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels
{
    public class DishesViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime LastUpdatedOn { get; set; }
        public List<IngredientsViewModel> Ingredients { get; set; } 
    }
}
