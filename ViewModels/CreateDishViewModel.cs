using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ViewModels
{
    public class CreateDishViewModel
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public List<CreateDishIngredientsViewModel> Ingredients { get; set; }
    }
    public class CreateDishIngredientsViewModel
    {
        public int Id { get; set; }

        public double Amount { get; set; }
    }
}
