using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ShoppingCart.Model;
using ShoppingCart.Services;

namespace ShoppingCart.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ItemController : Controller
    {
        private IMemoryCache _cache;

        private static IProductService productService;

        public ItemController(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
            if (productService == null)
            {
                productService = new ProductService(memoryCache);
            }
        }
        [HttpGet]
        public IEnumerable<Product> GeAllItems()
        {
            return productService.GeAllItems();
        }
        [HttpGet("GetByName/{name}")]
        public Product GetItemByName(string name)
        {
            return productService.GetItemByName(name);
        }
        [HttpGet("Get/{id}")]
        public Product GetItemById(int? id)
        {
            return productService.GetItemById(id);
        }
    }
}