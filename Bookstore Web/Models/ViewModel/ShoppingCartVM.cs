namespace Bookstore_Web.Models.ViewModel
{
    public class ShoppingCartVM
    {
        public IEnumerable<ShoppingCart> ShoppingCarts { get; set; }
        public OrderHeader orderHeader { get; set; }
    }
}
