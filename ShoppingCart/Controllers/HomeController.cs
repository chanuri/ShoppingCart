using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ShoppingCart.Model;
using ShoppingCart.Services;
//Developer chanurics@gmail.com

namespace ShoppingCart.Controllers
{
    //[Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class HomeController : Controller
    {
        private IMemoryCache _cache;
        private static IShoppingCartService shoppingcartService;
        private static IProductService productService;
        public HomeController(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
            if(shoppingcartService==null)
            {
                shoppingcartService = new ShoppingCartService(memoryCache);
                productService = new ProductService(memoryCache);
            }
            
        }
        [HttpGet]
        [ActionName("test")]
        //http://localhost:53130/api/Home/test
        public IEnumerable<string> Test()
        {

            return new string[]{ "shopping cart web portal running"};
        }
        [HttpGet]
        [ActionName("getAll")]
        ////http://localhost:53130/api/Home/getAll
        public IEnumerable<Product> GeAllItems()
        {

            return  productService.GeAllItems();
        }
        [HttpGet]
        [ActionName("getByName/{name}")]
        //http://localhost:53130/api/Home/getByName/test1
        public Product GetItemByName(string name)
        {
            return productService.GetItemByName(name);
        }
        [HttpGet]
        [ActionName("getById/{id}")]
        //http://localhost:53130/api/Home/getById/2
        public Product GetItemById(int? id)
        {
            return productService.GetItemById(id);
        }
        [HttpPost]
        [ActionName("AddToCart")]
        public bool AddToCart(int? id, int? qty)
        {
            var cart= shoppingcartService.GetCart(1);
            var product=productService.GetItemById(id);
            var orderitem = new OrderItems()
            {
                CartId = cart.Id,
                DetailId = cart.Items.Count() + 1,
                Product=product,
                Qty=(int)qty

            };
            return shoppingcartService.AddToCart(orderitem);
        }
        [HttpGet]
        [ActionName("GetCart/")]
        public Cart GetCart()
        {
            var cart = shoppingcartService.GetCart(1);
            return cart;
        }
        [HttpPut]
        [ActionName("UpdateQty")]
        public bool UpdateQty(int? id, int? qty)
        {
            var cart = shoppingcartService.GetCart(1);
            var product = productService.GetItemById(id);
            var orderitem = new OrderItems()
            {
                CartId = cart.Id,
                DetailId = cart.Items.Count() + 1,
                Product = product,
                Qty = (int)qty

            };
            return shoppingcartService.AddToCart(orderitem);
        }
        [HttpDelete]
        [ActionName("RemoveItem")]
        public bool RemoveItem(int? id, int? qty)
        {
            var cart = shoppingcartService.GetCart(1);
            var product = productService.GetItemById(id);
            var orderitem = new OrderItems()
            {
                CartId = cart.Id,
                DetailId = cart.Items.Count() + 1,
                Product = product,
                Qty = (int)qty

            };
            return shoppingcartService.AddToCart(orderitem);
        }
        //// POST api/Home
        //[HttpPost]
        //public bool InsertItem(Product product)
        //{
        //    return false;
        //    //return productService.InsertProduct(product);
        //}
    }
}