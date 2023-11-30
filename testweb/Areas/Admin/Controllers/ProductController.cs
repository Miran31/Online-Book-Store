using Microsoft.AspNetCore.Mvc;
using test.dataAccess.Repository.IRepository;
using test.Models;

namespace testweb.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;

        }
        public IActionResult Index()
        {
            List<Product> ProductList = _productRepository.GetAll().ToList();
            return View(ProductList);
        }
        public IActionResult Upsert(int? id)
        {
            if(id == null || id == 0)
            {
                return View();
            }
            Product product = _productRepository.Get(u=>u.Id == id);
            return View(product);
        }
        [HttpPost]
        public IActionResult Upsert(Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.Id == 0)
                {
                    _productRepository.Add(product);
                }
                else
                {
                    _productRepository.Update(product);
                }
                _productRepository.Save();
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product product)
        {          
            if (ModelState.IsValid)
            {
                _productRepository.Add(product);
                _productRepository.Save();
                TempData["Success"] = "Product Cteated Sussessfully";
                return RedirectToAction("Index");
            }
            return View();  
        }
        public IActionResult Update(int id) 
        {
            Product product = _productRepository.Get(u => u.Id == id);
            return View(product);
        }
        [HttpPost]
        public IActionResult Update(Product product)
        {         
            if (ModelState.IsValid)
            {
                _productRepository.Update(product);
                _productRepository.Save();
                TempData["Success"] = "Product Cteated Sussessfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if(id == null || id==0)
            {
                return NotFound();
            }
            Product products = _productRepository.Get(u=>u.Id == id);
            if (products == null)
            {
                return NotFound();
            }
            _productRepository.Remove(products);
            _productRepository.Save();
            return RedirectToAction("Index");
        }
    }
}
