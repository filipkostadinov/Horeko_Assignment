using DataAccess.Interfaces;
using DataModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataAccess
{
    public class DataLoader : IDataLoader
    {
        public IEnumerable<IngredientDto> GetAllIngredients()
        {
            string ingredients = File.ReadAllText("ingredients-sample-data.json");
            var result = JsonConvert.DeserializeObject<IEnumerable<IngredientDto>>(ingredients);
            return result;
        }
        public IEnumerable<DishDto> GetAllDishes()
        {
            string dishes = File.ReadAllText("dishes-sample-data.json");

            var result = JsonConvert.DeserializeObject<IEnumerable<DishDto>>(dishes);
            return result;
        }
        public void Create(IEnumerable<DishDto> data)
        {
            string result = JsonConvert.SerializeObject(data);
            File.WriteAllText("dishes-sample-data.json", result);
        }
        
    }
}
