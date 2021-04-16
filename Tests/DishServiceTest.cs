using DataAccess;
using DataModels;
using NUnit.Framework;
using Services;
using Services.Exceptions;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using ViewModels;

namespace Tests
{
    [TestFixture]
    public class DishServiceTest
    {
        private IDishService _dishService;
        private IIngredientsService _ingredientsService;

        [SetUp]
        public void SetUp()
        {
            _dishService = new DishService(new FakeDataLoader());
            _ingredientsService = new IngredientsService(new FakeDataLoader());
        }

        [Test]
        public void GetAllDishes_NoParameters_AllDishes()
        {
            //Arrange
            int expectedResult = 2;
            string name = null;
            DateTime? date = null;

            //Act
            IEnumerable<DishesViewModel> result = _dishService.GetAllDishes(name,date);

            // Assert
            Assert.AreEqual(expectedResult, result.ToList().Count);
        }

        [Test]
        public void GetAllDishes_ByName_AllDishes()
        {
            //Arrange
            int expectedResult = 1;
            string name = "Test Dish 1";
            DateTime? date = null;

            //Act
            IEnumerable<DishesViewModel> result = _dishService.GetAllDishes(name, date);

            // Assert
            Assert.AreEqual(expectedResult, result.ToList().Count);
        }

        [Test]
        public void GetAllDishes_ByDate_AllDishes()
        {
            //Arrange
            int expectedResult = 1;
            string name = null;
            DateTime? date = Convert.ToDateTime("2021-04-08T12:19:06.67");

            //Act
            IEnumerable<DishesViewModel> result = _dishService.GetAllDishes(name, date);

            // Assert
            Assert.AreEqual(expectedResult, result.ToList().Count);
        }

        [Test]
        public void GetAllDishes_ByNameAndDate_AllDishes()
        {
            //Arrange
            int expectedResult = 1;
            string name = "Test Dish 2";
            DateTime? date = Convert.ToDateTime("2021-04-08T12:19:06.67");

            //Act
            IEnumerable<DishesViewModel> result = _dishService.GetAllDishes(name, date);

            // Assert
            Assert.AreEqual(expectedResult, result.ToList().Count);
        }

        [Test]
        public void GetDishById_ValidId_ValidDish()
        {
            //Arrange
            int expectedResult = 1;
            int id = 1;
            //Act
            DishViewModel result = _dishService.GetDishById(id);

            // Assert
            Assert.AreEqual(expectedResult, result.Id);
        }

        [Test]
        public void GetDishById_InvalidId_Exception()
        {
            //Arrange
            int id = 23;

            //Act / Assert   
            Assert.Throws<DishException>(() => _dishService.GetDishById(id));
        }

        [Test]
        public void CreateDish_ValidDish_ValidaId()
        {
            //Arrange
            int expectedResult = 3;
            CreateDishViewModel model = new CreateDishViewModel()
            {
                Name = "Test Dish 3",
                Ingredients = new List<CreateDishIngredientsViewModel>() 
                {
                    new CreateDishIngredientsViewModel()
                    {
                        Id = 2,
                        Amount = 2
                    }
                }
            };

            //Act
            int? result = _dishService.CreateDish(model);

            // Assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void CreateDish_InvalidDish_Exception()
        {
            //Arrange
            CreateDishViewModel model = new CreateDishViewModel()
            {
                Name = "Test Dish 2",
                Ingredients = new List<CreateDishIngredientsViewModel>()
                {
                    new CreateDishIngredientsViewModel()
                    {
                        Id = 2,
                        Amount = 2
                    }
                }
            };
            // Act / Assert
            Assert.Throws<Exception>(() => _dishService.CreateDish(model));
        }

        [Test]
        public void CreateDish_InvalidIngredient_Exception()
        {
            //Arrange
            CreateDishViewModel model = new CreateDishViewModel()
            {
                Name = "Test Dish 2",
                Ingredients = new List<CreateDishIngredientsViewModel>()
                {
                    new CreateDishIngredientsViewModel()
                    {
                        Id = 123,
                        Amount = 2
                    }
                }
            };
            // Act / Assert
            Assert.Throws<Exception>(() => _dishService.CreateDish(model));
        }

        [Test]
        public void GetDishesWithPrices_ValidDishesWithPrices_DishWithPrice()
        {
            //Arrange
            double expectedResult = 0.5 * 5;
            int id = 2;
            //Act
            PricesViewModel result = _dishService.GetDishesWithPrices().FirstOrDefault(x => x.Id == id);

            // Assert
            Assert.AreEqual(expectedResult, result.Price);
        }

        [Test]
        public void GetIngredientsUsage_IngredientsAmount_ValidIngredientsAmount()
        {
            //Arrange
            double expectedResult = 2 * 5;
            int id = 2;
            //Act
            IngredientsUsageViewModel result = _ingredientsService.GetIngredientsUsage().FirstOrDefault(x => x.Id == id);

            // Assert
            Assert.AreEqual(expectedResult, result.TotalAmount);
        }

        [Test]
        public void GetIngredientsUsage_IngredientsNumberOfDishes_ValidNumberOfDishes()
        {
            //Arrange
            double expectedResult = 2;
            int id = 2;
            //Act
            IngredientsUsageViewModel result = _ingredientsService.GetIngredientsUsage().FirstOrDefault(x => x.Id == id);

            // Assert
            Assert.AreEqual(expectedResult, result.NumberOfDishes);
        }
    }
}
