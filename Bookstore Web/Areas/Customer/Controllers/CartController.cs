using Bookstore_Web.Data.Repository.IRepository;
using Bookstore_Web.Models;
using Bookstore_Web.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace Bookstore_Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IApplicationUserRepository _applicationUserRepository;
        private ShoppingCartVM shoppingCartVM { get; set; }
        public CartController(IShoppingCartRepository shoppingCartRepository,IApplicationUserRepository applicationUserRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _applicationUserRepository = applicationUserRepository;
            
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            shoppingCartVM = new()
            {
                ShoppingCarts = _shoppingCartRepository.GetAll(u => u.ApplicationUserId == userId, includeProperty: "Product"),
                orderHeader = new()
            };
            

            foreach(var carts in shoppingCartVM.ShoppingCarts)
            {
                carts.Price = GetOrderTotal(carts);
                shoppingCartVM.orderHeader.OrderTotal += (carts.Price * carts.Count);
            }

            return View(shoppingCartVM);
        }
        public IActionResult plus(int cartid)
        {
            ShoppingCart Curcount = _shoppingCartRepository.Get(u => u.ProductId == cartid);
            Curcount.Count += 1;
            _shoppingCartRepository.Update(Curcount);
            _shoppingCartRepository.Save();
            return RedirectToAction("Index");
        }
        public IActionResult minus(int cartid)
        {
            ShoppingCart Curcount = _shoppingCartRepository.Get(u => u.ProductId == cartid);
            if(Curcount.Count > 1)
            {
                Curcount.Count -= 1;
                _shoppingCartRepository.Update(Curcount);
                _shoppingCartRepository.Save();
            }
            else
            {
                _shoppingCartRepository.Remove(Curcount);
                _shoppingCartRepository.Save();
            }
            return RedirectToAction("Index");
        }
        public IActionResult remove(int cartid)
        {
            ShoppingCart Curcount = _shoppingCartRepository.Get(u => u.ProductId == cartid);
            _shoppingCartRepository.Remove(Curcount);
            _shoppingCartRepository.Save();
            return RedirectToAction("Index");
        }

        public IActionResult Summery()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            shoppingCartVM = new()
            {
                ShoppingCarts = _shoppingCartRepository.GetAll(u => u.ApplicationUserId == userId, includeProperty: "Product"),
				orderHeader = new()
			};

            shoppingCartVM.orderHeader.ApplicationUser = _applicationUserRepository.Get(u => u.Id == userId);
            shoppingCartVM.orderHeader.Name = shoppingCartVM.orderHeader.ApplicationUser.Name;
            shoppingCartVM.orderHeader.City = shoppingCartVM.orderHeader.ApplicationUser.City;
            shoppingCartVM.orderHeader.StreetAddress = shoppingCartVM.orderHeader.ApplicationUser.StreetAddress;
            shoppingCartVM.orderHeader.PostalCode = shoppingCartVM.orderHeader.ApplicationUser.PostalCode;
            shoppingCartVM.orderHeader.PhoneNumber = shoppingCartVM.orderHeader.ApplicationUser.PhoneNumber;
            shoppingCartVM.orderHeader.State = shoppingCartVM.orderHeader.ApplicationUser.State;

            foreach (var carts in shoppingCartVM.ShoppingCarts)
            {
                carts.Price = GetOrderTotal(carts);
                shoppingCartVM.orderHeader.OrderTotal += (carts.Price * carts.Count);
            }
            return View(shoppingCartVM);
        }

        private double GetOrderTotal(ShoppingCart shoppingCart)
        {
            double total = 0;
            if (shoppingCart.Count <= 50)
            {
                total = shoppingCart.Product.ListPrice;
            }
            else
            {
                if(shoppingCart.Count <= 100)
                {
                    total=shoppingCart.Product.Price50;
                }
                else
                {
                    total = shoppingCart.Product.Price100;
                }
            }
            return total;
        }
    }
}
