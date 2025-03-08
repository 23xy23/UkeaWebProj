using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BootstrapProject.Models;
using BootstrapProject.Data;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace BootstrapProject.Controllers
{
    public class InventoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<InventoryController> _logger;

        public InventoryController(ApplicationDbContext context, ILogger<InventoryController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var totalCategories = await _context.Categories.CountAsync();
            var totalProducts = await _context.Products.CountAsync();
            var lowStockCount = await _context.Products.CountAsync(p => p.Quantity < p.MinStockLevel);

            var inventoryItems = await _context.Products.Include(p => p.Category).ToListAsync();

            var model = new InventoryViewModel
            {
                TotalCategories = totalCategories,
                TotalProducts = totalProducts,
                LowStockCount = lowStockCount,
                InventoryItems = inventoryItems
            };

            return View(model);
        }

    [HttpGet]
    public async Task<IActionResult> GetProductData(int productId)
    {
        var product = await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.ProductID == productId);

        if (product == null)
            return NotFound(); // Return 404 if product not found

        return Json(product);
    }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductID,ProductName,SKU,imageUrl,Quantity,UnitPrice,CostPrice,DateReceived,DateSold,Condition,MinStockLevel,ReorderLevel,LeadTime,BatchNo,Notes,BrandID,CategoryID,SupplierID")] Product product)
        {
            _logger.LogInformation("Received BrandID: {BrandID} (Type: {BrandIDType}), CategoryID: {CategoryID} (Type: {CategoryIDType}), SupplierID: {SupplierID} (Type: {SupplierIDType})", 
                                product.BrandID, product.BrandID.GetType().Name,
                                product.CategoryID, product.CategoryID.GetType().Name,
                                product.SupplierID, product.SupplierID.GetType().Name);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(product);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while creating the product.");
                    ModelState.AddModelError(string.Empty, "An error occurred while creating the product. Please try again.");
                }
            }
            else
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    _logger.LogError(error.ErrorMessage);
                }
            }

            

            return View(product);
        }




        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductID,ProductName,SKU,imageUrl,Quantity,UnitPrice,CostPrice,DateReceived,DateSold,Condition,MinStockLevel,ReorderLevel,LeadTime,BatchNo,Notes,BrandID,CategoryID,SupplierID")] Product product)
        {
            _logger.LogInformation("Edit method called for ProductID: {ProductID}", id);
            _logger.LogInformation("Received ProductID in model: {ProductID}", product.ProductID);

            if (id != product.ProductID)
            {
                _logger.LogWarning("Edit operation failed: Product ID mismatch");
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Product updated successfully: {ProductID}", product.ProductID);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    _logger.LogError(ex, "Concurrency exception while updating product: {ProductID}", product.ProductID);
                    if (!ProductExists(product.ProductID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Exception while updating product: {ProductID}", product.ProductID);
                    ModelState.AddModelError(string.Empty, "An error occurred while updating the product. Please try again.");
                }
            }
            else
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    _logger.LogError(error.ErrorMessage);
                }
            }

            return View(product);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var obj = _context.Products.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _context.Products.Remove(obj);
            _context.SaveChanges();
            TempData["success"] = "Product deleted successfully";

            return Ok(); // Return a success response
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductID == id);
        }
        
    }
}