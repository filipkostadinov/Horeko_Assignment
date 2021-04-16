using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Services;
using Services.Exceptions;
using Services.Interfaces;
using ViewModels;

namespace Horeko_AssignmentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IDishService _dishService;
        public DishController(IDishService dishService)
        {
            _dishService = dishService;
        }

        // GET: api/Dish
        [HttpGet]
        public ActionResult<IEnumerable<DishesViewModel>> Get(string name, DateTime? date = null)
        {
            try
            {
                return Ok(_dishService.GetAllDishes(name, date));
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Dish/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult Get(int id)
        {
            try
            {
                DishViewModel model = _dishService.GetDishById(id);
                return Ok(model);
            }
            catch (DishException ex)
            {
                Log.Error($"Error {ex.DishId} : {ex.Message}");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return BadRequest("Something went wrong");
            }
        }

        [HttpGet("price")]
        public ActionResult GetPrices()
        {
            try
            {
                return Ok(_dishService.GetDishesWithPrices());
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return BadRequest("Something went wrong");
            }
        }

        // POST: api/Dish
        [HttpPost]
        public ActionResult Post([FromBody] CreateDishViewModel model)
        {
            try
            {
                int? id = _dishService.CreateDish(model);
                Log.Information("Dish is created");
                return Ok(id);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return StatusCode(409, ex.Message);
            }
        }
    }
}
