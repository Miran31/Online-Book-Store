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
    public class CompanyController : Controller
    {
        
        private readonly ICompanyRepository _CompanyRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CompanyController(ICompanyRepository CompanyRepository, ICategoryRepository categoryRepository, IWebHostEnvironment webHostEnvironment)
        {
            _CompanyRepository = CompanyRepository;
            _categoryRepository = categoryRepository;
            _webHostEnvironment = webHostEnvironment;

        }
        public IActionResult Index()
        {
            List<Company> CompanyList = _CompanyRepository.GetAll().ToList();
            return View(CompanyList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Company company)
        {
            if (ModelState.IsValid)
            {
                _CompanyRepository.Add(company);
                _CompanyRepository.Save();
                TempData["Success"] = "Company Cteated Sussessfully";
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
        //    Company Company = _CompanyRepository.Get(u=>u.Id == id);
        //    return View(Company);
        //}
        //[HttpPost]
        //public IActionResult Upsert(Company Company)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (Company.Id == 0)
        //        {
        //            _CompanyRepository.Add(Company);
        //        }
        //        else
        //        {
        //            _CompanyRepository.Update(Company);
        //        }
        //        _CompanyRepository.Save();
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}
        public IActionResult Update(int id)
        {
            Company Companys = _CompanyRepository.Get(u => u.Id == id);
            return View(Companys);
        }
        [HttpPost]
        public IActionResult Update(Company company)
        {
            if (ModelState.IsValid)
            { 
                _CompanyRepository.Update(company);
                _CompanyRepository.Save();
                TempData["Success"] = "Company Updated Sussessfully";
                return RedirectToAction("Index");
            }
            return View(company);
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> CompanyList = _CompanyRepository.GetAll().ToList();
            return Json(new { data = CompanyList });
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var CompanytobeDeleted = _CompanyRepository.Get(u => u.Id == id);
            if (CompanytobeDeleted == null)
            {
                return Json(new { success = false, message = "error while deleting" });
            }
            _CompanyRepository.Remove(CompanytobeDeleted);
            _CompanyRepository.Save();
            return Json(new { success = true, message = "Company deleted" });
        }
        #endregion

    }
}
