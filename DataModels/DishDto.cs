using System;
using System.Collections.Generic;
using System.Text;

namespace DataModels
{
    public class DishDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public DateTime UpdatedOn { get; set; }
        public IEnumerable<DishIngredientsDto> Ingredients { get; set; }
    }
}
