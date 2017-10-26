namespace ShoppingCart.Model
{
    public class OrderItems
    {
        public int CartId { get; set; }
        public int DetailId { get; set; }
        public Product Product { get; set; }
        public int Qty { get; set; }
    }
}