using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using ViewModels;

namespace Horeko_AssignmentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly IIngredientsService _ingredientsService;
        public IngredientsController(IIngredientsService ingredientsService)
        {
            _ingredientsService = ingredientsService;
        }
        // GET: api/Ingredients
        [HttpGet("usage")]
        public ActionResult<IEnumerable<IngredientsUsageViewModel>> Get()
        {
            return Ok(_ingredientsService.GetIngredientsUsage());
        }
    }
}
