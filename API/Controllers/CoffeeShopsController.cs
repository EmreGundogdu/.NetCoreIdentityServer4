using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoffeeShopsController : ControllerBase
    {
        private readonly ICoffeeShopService coffeeShopService;

        public CoffeeShopsController(ICoffeeShopService coffeeShopService)
        {
            this.coffeeShopService = coffeeShopService;
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var coffeeShops =await coffeeShopService.GetAll();
            return Ok(coffeeShops); 
        }
    }
}
