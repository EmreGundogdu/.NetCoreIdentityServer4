using API.Models;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class CoffeeShopService : ICoffeeShopService
    {
        private readonly ApplicationDbContext context;

        public CoffeeShopService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<List<CoffeeShopModel>> GetAll()
        {
            var coffeeShops = await (from shop in context.CoffeeShops select new CoffeeShopModel()
            {
                Id = shop.Id,
                Name = shop.Name,
                Address = shop.Address,
                OpeningHours = shop.OpeningHours
            }).ToListAsync();
            return coffeeShops;
        }
    }
}
