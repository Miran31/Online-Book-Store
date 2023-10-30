using Microsoft.AspNetCore.Mvc;
using test.dataAccess.Data;
using test.dataAccess.Repository.IRepository;
using test.Models;

namespace testweb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _catrepo;
        public CategoryController(ICategoryRepository catrepo)
        {
            _catrepo = catrepo;
        }
        public IActionResult Index()
        {
            List<Category> categoryList = _catrepo.GetAll().ToList();
            //categoryList.ForEach(Console.WriteLine);
            return View(categoryList);
        }
        public IActionResult Create() {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name","Cannot be same");
            }
            if (ModelState.IsValid)
            {
                _catrepo.Add(obj);
                _catrepo.save();
                TempData["Success"] = "Category Created Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int? id) {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryformDb = _catrepo.Get(u => u.ID == id);
            //Category? categoryfromDb1 = _db.Categories.FirstOrDefault(u=>u.ID == id);
            //Category? categoryformDb2 = _db.Categories.Where(u=>u.ID==id).FirstOrDefault();
            if (categoryformDb == null)
            {
                return NotFound();
            }
            return View(categoryformDb);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _catrepo.update(obj);
                _catrepo.save();
                TempData["Success"] = "Category Edited Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            Category? category = _catrepo.Get(u => u.ID == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? category = _catrepo.Get(u => u.ID == id);
            if (category == null)
            {
                return NotFound();
            }
            _catrepo.Remove(category);
            _catrepo.save();
            TempData["Success"] = "Category Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
