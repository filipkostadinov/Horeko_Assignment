using DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Interfaces
{
    public interface IDataLoader
    {
        public IEnumerable<IngredientDto> GetAllIngredients();
        public IEnumerable<DishDto> GetAllDishes();
        public void Create(IEnumerable<DishDto> data);
    }
}
