using DataAccess.Interfaces;
using DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests
{
    public class FakeDataLoader : IDataLoader
    {
        public List<DishDto> dishes = new List<DishDto>()
        {
            new DishDto(){
                Id = 1,
                Name = "Test Dish 1",
                UpdatedOn = Convert.ToDateTime("2021-03-08T12:19:06.67"),
                ParentId = 2,
                Ingredients = new List<DishIngredientsDto>() 
                { 
                    new DishIngredientsDto() 
                    {
                        IngredientId = 1,
                        Amount = 2
                    } 
                }
            },
            new DishDto(){
                Id = 2,
                Name = "Test Dish 2",
                UpdatedOn = Convert.ToDateTime("2021-04-08T12:19:06.67"),
                ParentId = 20,
                Ingredients = new List<DishIngredientsDto>()
                {
                    new DishIngredientsDto()
                    {
                        IngredientId = 2,
                        Amount = 5
                    }
                }
            },
        };

        public List<IngredientDto> ingredients = new List<IngredientDto>()
        {
            new IngredientDto(){
                Id = 1,
                Name = "Test ingredient 1",
                Price = 0.3
            },
            new IngredientDto(){
                Id = 2,
                Name = "Test ingredient 2",
                Price = 0.5
            },
        };

        public void Create(IEnumerable<DishDto> data)
        {
            dishes.AddRange(data);
        }

        public IEnumerable<DishDto> GetAllDishes()
        {
            return dishes;
        }

        public IEnumerable<IngredientDto> GetAllIngredients()
        {
            return ingredients;
        }
    }
}
