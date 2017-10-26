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
            var cartData = new List<Cart>();
            cartData.Add(mycart);
            CacheSet(cartData);
        }

        private List<Cart> CacheGet()
        {
            var cacheEntry = _cache.Get(CacheKeys.ShoppingCartEntry) as List<Cart>;
            return cacheEntry;
        }
        public bool CacheSet(List<Cart> itemList)
        {
            _cache.Set(CacheKeys.ShoppingCartEntry, itemList);
            return true;
        }
        public bool CacheSet(Cart cart)
        {
            var oldCartList = CacheGet();
            oldCartList.Remove(oldCartList.FirstOrDefault(r => r.Id == cart.Id));
            oldCartList.Add(cart);
            CacheSet(oldCartList);
            return true;
        }
        public Cart GetCart(int? id)
        {
            // for the perpose of excersize there should be a one cart
            return CacheGet().FirstOrDefault(row=>row.Id==id);
        }
        
        public bool AddToCart(int? id,OrderItems item)
        {
            var cart = GetCart(id);
            if (item != null && item.Product.Id > 0 && item.Qty > 0)
            {
                cart.Items.Add(item);
                CacheSet(cart);
                return true;
            }
            return false;
        }

        public bool UpdateCartItem(int? id,int? itemId, int? qty)
        {
            var cart = GetCart(id);
            var item = cart.Items.FirstOrDefault(row => row.Product.Id == itemId);
            cart.Items.Remove(item);
            if (item!=null)
            {
                item.Qty = (int)qty;
                cart.Items.Add(item);
                CacheSet(cart);
                return true;
            }
            return false;            

        }

        public bool RemoveItem(int? id, int? itemId)
        {
            var cart = GetCart(id);
            var item = cart.Items.FirstOrDefault(row => row.Product.Id == itemId);
            cart.Items.Remove(item);
            CacheSet(cart);
            return true;
        }
        
    }
}
