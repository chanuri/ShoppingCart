using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart.Model
{
    public class Cart
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public List<OrderItems> Items { get; set; }
    }
}
