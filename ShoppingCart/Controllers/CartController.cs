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
    [Route("api/Cart")]
    public class CartController : Controller
    {
        IMemoryCache _cache;
        private static IShoppingCartService shoppingCartService;
        private static IProductService productService;
        public CartController(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
            if(shoppingCartService==null)
            {
                shoppingCartService = new ShoppingCartService(memoryCache);
                productService = new ProductService(memoryCache);
            }
        }
        [HttpGet]
        public Cart GetCart()
        {
            var cart = shoppingCartService.GetCart(1); // only one cart maintain for the excersize perpose
            return cart;
        }
        [HttpPut("Update")]
        public bool UpdateQty([FromBody]int? cartId, [FromBody]int? ItemId, [FromBody]int? qty)
        {
            var cart = shoppingCartService.GetCart(cartId);
            var product = productService.GetItemById(ItemId);
            var orderitem = new OrderItems()
            {
                CartId = cart.Id,
                DetailId = cart.Items.Count() + 1,
                Product = product,
                Qty = (int)qty

            };
            return shoppingCartService.AddToCart(cartId,orderitem);
        }

        [HttpDelete]
        [ActionName("Delete")]
        public bool RemoveItem([FromBody]int? cartId, [FromBody]int? ItemId)
        {
            return shoppingCartService.RemoveItem(cartId, ItemId);
        }
    }
}