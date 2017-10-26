using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingCart.Model;

namespace ShoppingCart.Services
{
    public interface IShoppingCartService
    {
        Cart GetCart(int? id);
        bool AddToCart(int? id,OrderItems item);
        bool UpdateCartItem(int? id,int? itemId, int? qty);
        bool RemoveItem(int? id,int? itemId);
    }
}
