using DataAccess.Interfaces;
using DataModels;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModels;

namespace Services
{
    public class IngredientsService : IIngredientsService
    {
        private readonly IDataLoader _dataLoader;
        private readonly DishService _dishService;

        private IEnumerable<DishDto> _dishData;
        private IEnumerable<IngredientDto> _ingredientData;
        public IngredientsService(IDataLoader dataLoader)
        {
            _dataLoader = dataLoader;
            _dishService = new DishService(_dataLoader);
            _dishData = dataLoader.GetAllDishes();
            _ingredientData = dataLoader.GetAllIngredients();
        }
        public IEnumerable<IngredientsUsageViewModel> GetIngredientsUsage()
        {
            var result = _ingredientData.Select(x => new IngredientsUsageViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                TotalAmount = GetAmounts(x.Id).Sum(),
                NumberOfDishes = GetAmounts(x.Id).Count()
            });
            return result; 
        }

        // it will get all the amount of specific ingredient, including when it is a parent dish
        private List<double> GetAmounts(int id)
        {
            var amounts = new List<double>();
            foreach (var item in _dishData)
            {
                var ingredients = _dishService.GetAllIngredientByDish(item).Where(x => x.IngredientId == id);
                if(ingredients.Count() > 0)
                {
                    amounts.Add(ingredients.Sum(x => x.Amount));
                }
            }

            return amounts;
        }
    }
}
