using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels
{
    public class DishViewModel : DishesViewModel
    {
        public ParentDishViewModel ParentDish { get; set; }
    }
    public class ParentDishViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
