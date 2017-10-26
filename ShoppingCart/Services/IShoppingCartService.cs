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
        bool AddToCart(OrderItems item);
        bool UpdateCartItem(int itemId, int qty);
        bool RemoveItem(int itemId);
    }
}
