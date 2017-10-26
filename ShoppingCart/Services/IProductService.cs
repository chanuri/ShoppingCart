using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShoppingCart.Model;

namespace ShoppingCart.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GeAllItems();
        Product GetItemByName(string name);
        Product GetItemById(int? id);
        //bool InsertProduct(Product product);
    }
}
