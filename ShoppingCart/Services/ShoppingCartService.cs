using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using ShoppingCart.Model;

namespace ShoppingCart.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        public IMemoryCache _cache;

        public ShoppingCartService(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
            Cart mycart = new Cart()
            {
                Customer = new Customer()
                {
                    Email = "navvennish@gmail.com",
                    Id = 11,
                    Name = "Naveen"
                },
                Id = 1   ,
                Items=new List<OrderItems>()
            };
            mycart.Items.Add(new OrderItems()
            {
                CartId = 1,
                DetailId = 1,
                Product = new Product() { Code = "test1", Id = 4, Price = 100 },
                Qty = 4
            });
            CacheSet(mycart);
        }

        private Cart CacheGet()
        {
            var cacheEntry = _cache.Get(CacheKeys.ShoppingCartEntry) as Cart;
            return cacheEntry;
        }
        public bool CacheSet(Cart item)
        {
            _cache.Set(CacheKeys.ShoppingCartEntry, item);
            return true;
        }

        public Cart GetCart(int? id)
        {
            // for the perpose of excersize there should be a one cart
            return CacheGet();
        }

        public bool AddToCart(OrderItems item)
        {
            var cart= CacheGet();
            if (item != null && item.Product.Id > 0 && item.Qty > 0)
            {
                cart.Items.Add(item);
                return true;
            }
            return false;
        }

        public bool UpdateCartItem(int itemId, int qty)
        {
            var cart = CacheGet();
            var item = cart.Items.FirstOrDefault(row => row.Product.Id == itemId);
            cart.Items.Remove(item);
            if (item!=null)
            {
                item.Qty = qty;
                cart.Items.Add(item);
                CacheSet(cart);
                return true;
            }
            return false;
            

        }

        public bool RemoveItem(int itemId)
        {
            var cart = CacheGet();
            var item = cart.Items.FirstOrDefault(row => row.Product.Id == itemId);
            cart.Items.Remove(item);
            CacheSet(cart);
            return true;
        }
        
    }
}
