using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BootstrapProject.Models;
using BootstrapProject.Data;


namespace UKEA.Controllers
{
    public class TestController : Controller
    {

        // Initialize Database
        public ApplicationDbContext Database
        {
            get;
        }

        private readonly ApplicationDbContext _db;

        public TestController(ApplicationDbContext db)
        {
            _db = db;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            IEnumerable<Brand> objCategoryList = _db.Brands;
            return View(objCategoryList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Brand brand)
        {
            if (ModelState.IsValid)
            {
                _db.Brands.Add(brand);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(brand);
        }

         //GET
        public IActionResult Edit(int? id)
        {
            if(id==null || id == 0)
            {
                return NotFound();
            }
            var brandFromDb = _db.Brands.Find(id);
            //var categoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);
            if (brandFromDb == null)
            {
                return NotFound();
            }

            return View(brandFromDb);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Brand obj)
        {
            if (ModelState.IsValid)
                {
                    var brandFromDb = await _db.Brands.FindAsync(obj.BrandID);
                    if (brandFromDb == null)
                    {
                        return NotFound();
                    }
                    
                    // Update properties
                    brandFromDb.BrandName = obj.BrandName;
                    brandFromDb.Description = obj.Description;

                    // Save changes
                    _db.Brands.Update(brandFromDb);
                    await _db.SaveChangesAsync();

                    TempData["success"] = "Brand updated successfully";
                    return RedirectToAction(nameof(Index));
                }
            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var brandFromDb = _db.Brands.Find(id);

            if (brandFromDb == null)
            {
                return NotFound();
            }

            return View(brandFromDb);
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Brands.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Brands.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Brand deleted successfully";
            return RedirectToAction("Index");

        }
    }
}