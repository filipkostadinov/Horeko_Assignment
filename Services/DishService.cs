using DataAccess.Interfaces;
using DataModels;
using Services.Exceptions;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModels;

namespace Services
{
    public class DishService : IDishService
    {
        private readonly IDataLoader _dataLoader;

        private IEnumerable<DishDto> _dishData;
        private IEnumerable<IngredientDto> _ingredientData;
        public DishService(IDataLoader dataLoader)
        {
            _dataLoader = dataLoader;
            _dishData = dataLoader.GetAllDishes();
            _ingredientData = dataLoader.GetAllIngredients();
        }
        public IEnumerable<DishesViewModel> GetAllDishes(string name, DateTime? date)
        {
            try
            {
                var result = _dishData.Select(x => new DishesViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    LastUpdatedOn = x.UpdatedOn,
                    Ingredients = x.Ingredients.Select(i => new IngredientsViewModel()
                    {
                        Id = i.IngredientId,
                        Name = _ingredientData.Where(d => d.Id == i.IngredientId).Select(n => n.Name).SingleOrDefault(),
                        Amount = i.Amount
                    }).ToList()
                });

                if (name != null)
                    result = result.Where(x => x.Name == name);

                // to be more clearly it filters only by day not by hours,minutes,seconds
                if (date != null)
                    result = result.Where(x => x.LastUpdatedOn.Date == date.Value.Date); 

                return result;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public DishViewModel GetDishById(int id)
        {
            try
            {
                DishDto dish = _dishData.FirstOrDefault(x => x.Id == id);
                DishViewModel dishModel = new DishViewModel()
                {
                    Id = dish.Id,
                    Name = dish.Name,
                    LastUpdatedOn = dish.UpdatedOn,
                    ParentDish = _dishData.Where(x => x.Id == dish.ParentId).Select(x => new ParentDishViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name
                    }).FirstOrDefault(),
                    Ingredients = GetAllIngredientByDish(dish).Select(i => new IngredientsViewModel()
                    {
                        Id = i.IngredientId,
                        Name = _ingredientData.Where(d => d.Id == i.IngredientId).Select(n => n.Name).SingleOrDefault(),
                        Amount = i.Amount
                    }).ToList()
                };
                return dishModel;
            }
            catch (Exception)
            {
                throw new DishException(id, $"Dish with id: {id} is not found.");
            }
        }

        public int? CreateDish(CreateDishViewModel model)
        {
            var data = _dishData.ToList();
             
            if(data.Select(x => x.Name).Contains(model.Name))
            {
                throw new Exception($"Dish with a name: { model.Name } already exists.");
            }

            DishDto newDish = new DishDto()
            {
                Id = GenerateId(data),
                Name = model.Name,
                UpdatedOn = DateTime.UtcNow
            };

            try
            {
                newDish.Ingredients = model.Ingredients.Select(x => new DishIngredientsDto()
                {
                    IngredientId = _ingredientData.FirstOrDefault(i => i.Id == x.Id).Id,
                    Amount = x.Amount
                }).ToList();
            }
            catch (Exception)
            {
                throw new Exception("One of the ingredients doesn't exists.");
            }

            data.Add(newDish);
            _dataLoader.Create(data);

            return newDish.Id;
        }

        public IEnumerable<PricesViewModel> GetDishesWithPrices()
        {
            try
            {
                List<PricesViewModel> dishPrices = new List<PricesViewModel>();

                foreach (var item in _dishData)
                {
                    var prices = GetAllIngredientByDish(item).Select(x => new
                    {
                        Price = _ingredientData.Where(y => y.Id == x.IngredientId).Select(z => z.Price).FirstOrDefault() * x.Amount
                    }).Select(x => x.Price);

                    PricesViewModel priceModel = new PricesViewModel()
                    {
                        Id = item.Id,
                        Name = item.Name,
                        Price = prices.Sum()
                    };
                    dishPrices.Add(priceModel);
                }
                return dishPrices;
            }
            catch (Exception)
            {
                throw;
            }

        }

        //  recursion method that returns all the ingredients for a dish including its parent dishes
        internal IEnumerable<DishIngredientsDto> GetAllIngredientByDish(DishDto dish)
        {
            if (dish == null)
            {
                return new List<DishIngredientsDto>();
            }
            else
            {
                var ingredients = new List<DishIngredientsDto>();
                ingredients.AddRange(dish.Ingredients);
                ingredients.AddRange(GetAllIngredientByDish(_dishData.Where(x => x.Id == dish.ParentId).FirstOrDefault()));

                return ingredients;
            }
        }

        // generate an uniqe ID
        // if the last id + 1 is contained in the data it will generate random number
        // if that random number also is contained in the data will get into recursion until its find a unique number
        // this is just for testing purposes, in practice it should use GUID (aka UUID) which are stored in database 
        private int GenerateId(IEnumerable<DishDto> data) 
        {
            var ids = data.Select(x => x.Id);
            if (!ids.Contains(ids.Last() + 1))
            {
                return ids.Last() + 1;
            }
            else
            {
                int random = new Random().Next(100); 
                if (ids.Contains(random))
                {
                    return GenerateId(data);
                }
                return random;
            }
        }
    }
}
