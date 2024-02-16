using Bookstore_Web.Data.Repository;
using Bookstore_Web.Data.Repository.IRepository;
using Bookstore_Web.Models;
using Bookstore_Web.Models.ViewModel;
using Bookstore_Web.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bookstore_Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository, IWebHostEnvironment webHostEnvironment)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _webHostEnvironment = webHostEnvironment;

        }
        public IActionResult Index()
        {
            List<Product> ProductList = _productRepository.GetAll(includeProperty: "Category").ToList();
            return View(ProductList);
        }
        public IActionResult Create()
        {
            ProductVM productVM = new()
            {
                CategoryList = _categoryRepository.GetAll().ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.ID.ToString(),
                }),
                product = new Product()
            };
            return View(productVM);
        }
        [HttpPost]
        public IActionResult Create(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");
                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    productVM.product.imageUrl = @"\images\product\" + fileName;
                }
                _productRepository.Add(productVM.product);
                _productRepository.Save();
                TempData["Success"] = "Product Cteated Sussessfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        //public IActionResult Upsert(int? id)
        //{
        //    if(id == null || id == 0)
        //    {
        //        return View();
        //    }
        //    Product product = _productRepository.Get(u=>u.Id == id);
        //    return View(product);
        //}
        //[HttpPost]
        //public IActionResult Upsert(Product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (product.Id == 0)
        //        {
        //            _productRepository.Add(product);
        //        }
        //        else
        //        {
        //            _productRepository.Update(product);
        //        }
        //        _productRepository.Save();
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}
        public IActionResult Update(int id)
        {
            ProductVM productVM = new()
            {
                CategoryList = _categoryRepository.GetAll().ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.ID.ToString(),
                }),
            };
            Product products = _productRepository.Get(u => u.Id == id);
            productVM.product = products;
            return View(productVM);
        }
        [HttpPost]
        public IActionResult Update(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");
                    if (!string.IsNullOrEmpty(productVM.product.imageUrl))
                    {
                        var oldimagePath = Path.Combine(wwwRootPath, productVM.product.imageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldimagePath))
                        {
                            System.IO.File.Delete(oldimagePath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    productVM.product.imageUrl = @"\images\product\" + fileName;
                }
                _productRepository.Update(productVM.product);
                _productRepository.Save();
                TempData["Success"] = "Product Updated Sussessfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> ProductList = _productRepository.GetAll(includeProperty: "Category").ToList();
            return Json(new { data = ProductList });
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var producttobeDeleted = _productRepository.Get(u => u.Id == id);
            if (producttobeDeleted == null)
            {
                return Json(new { success = false, message = "error while deleting" });
            }
            var oldimagePath = Path.Combine(_webHostEnvironment.WebRootPath, producttobeDeleted.imageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldimagePath))
            {
                System.IO.File.Delete(oldimagePath);
            }
            _productRepository.Remove(producttobeDeleted);
            _productRepository.Save();
            return Json(new { success = true, message = "product deleted" });
        }
        #endregion

    }
}
