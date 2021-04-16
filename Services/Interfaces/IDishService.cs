using DataModels;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModels;

namespace Services.Interfaces
{
    public interface IDishService
    {
        IEnumerable<DishesViewModel> GetAllDishes(string name, DateTime? date);
        DishViewModel GetDishById(int id);
        IEnumerable<PricesViewModel> GetDishesWithPrices();
        int? CreateDish(CreateDishViewModel model);
    }
}
