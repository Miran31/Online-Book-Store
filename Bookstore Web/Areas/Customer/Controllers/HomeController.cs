using Bookstore_Web.Data.Repository.IRepository;
using Bookstore_Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace Bookstore_Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository _productRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public HomeController(ILogger<HomeController> logger, IProductRepository productRepository, IShoppingCartRepository shoppingCartRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
            _shoppingCartRepository = shoppingCartRepository;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> products = _productRepository.GetAll(includeProperty: "Category");
            return View(products);
        }
        public IActionResult Details(int id)
        {
            ShoppingCart shoppingCart = new()
            {
                Product = _productRepository.Get(u => u.Id == id, includeProperty: "Category"),
                Count = 1,
                ProductId= id
            };
            return View(shoppingCart);
        }
        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            shoppingCart.Id = 0;
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            shoppingCart.ApplicationUserId = userId;

            ShoppingCart shoppingFromDb = _shoppingCartRepository.Get(u=>u.ApplicationUserId== userId && u.ProductId==shoppingCart.ProductId);

            if(shoppingFromDb != null)
            {
                shoppingFromDb.Count += shoppingCart.Count;
                _shoppingCartRepository.Update(shoppingFromDb);
            }
            else
            {
                _shoppingCartRepository.Add(shoppingCart);
            }
            _shoppingCartRepository.Save();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
