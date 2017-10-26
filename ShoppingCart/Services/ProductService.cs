using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using ShoppingCart.Model;

namespace ShoppingCart.Services
{
    public class ProductService : IProductService
    {
        IMemoryCache _cache;
        public ProductService(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
            AddDefaultItems();
        }

        private void AddDefaultItems()
        {
            List<Product> productList = new List<Product>();

            productList.Add(new Product() { Code = "SAMPLE PRODUCT A", Id = 1, Price = 120 });
            productList.Add(new Product() { Code = "SAMPLE PRODUCT B", Id = 2, Price = 135 });
            productList.Add(new Product() { Code = "SAMPLE PRODUCT C", Id = 3, Price = 989 });
            productList.Add(new Product() { Code = "test1", Id = 4, Price = 100 });
            productList.Add(new Product() { Code = "test2", Id =5, Price = 200 });
            CacheSet(productList);
        }

        public IEnumerable<Product> GeAllItems()
        {
            var cacheItems = CacheGet();
            return cacheItems;
        }

        //public bool InsertProduct(Product product)
        //{
            
        //    var cacheEntry = CacheGet(); 
        //    //if(cacheEntry!=null)
        //    //{
        //    //    product.Id = cacheEntry.Max(r => r.Id) + 1;
        //    //}
        //    //else
        //    //{
        //    //    product.Id = 1;
        //    //}
        //    var result = CacheSet(product);
        //    return result;
        //}

        private List<Product> CacheGet()
        {
            var cacheEntry = _cache.Get(CacheKeys.ProductEntry) as List<Product>;
            return cacheEntry;
        }
        public bool CacheSet(List<Product> productList)
        {
            //var productList = CacheGet();
            //if (productList == null)
            //{
            //    productList = new List<Product>();
            //}
            //// productList.Add(product);
            _cache.Set(CacheKeys.ProductEntry, productList);
            return true;
        }

        public Product GetItemByName(string name)
        {
            var result = CacheGet().FirstOrDefault(row => row.Code == name);
            return result;
        }

        public Product GetItemById(int? id)
        {
            var result = CacheGet().FirstOrDefault(row => row.Id == id);
            return result;
        }
    }
}
