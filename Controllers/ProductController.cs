using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NipamInfotech_MachineTest.data;
using NipamInfotech_MachineTest.Models;

namespace NipamInfotech_MachineTest.Controllers
{
    public class ProductController : Controller
    {
        private readonly TestDbContext _context;
        public ProductController(TestDbContext context)
        {

            _context = context;

        }

        // GET: ProductController1
        public ActionResult Index(int page = 1)
        {
            int pageSize = 10;
            int totalRecords = _context.products.Count();
            int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var products = _context.products
                .Include(p => p.Category)
                .OrderBy(p => p.ProductId)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            return View(products);
        }


        public ActionResult AddEdit(int? id)
        {
            Product product = new Product();

            if (id.HasValue)
            {
                product = _context.products.Include(p => p.Category).FirstOrDefault(p => p.ProductId == id.Value);
                if (product == null)
                {
                    return NotFound();
                }
            }

            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "CategoryId", "CategoryName");
            return View(product);
        }

        // ProductController1/AddEdit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEdit(Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.ProductId == 0) 
                {
                    // Add new product
                    _context.products.Add(product);
                }
                else
                {
                    // Update existing product
                    var existingProduct = _context.products.Find(product.ProductId);
                    if (existingProduct == null)
                    {
                        return NotFound();
                    }
                    existingProduct.ProductName = product.ProductName;
                    existingProduct.CategoryId = product.CategoryId;
                    _context.Update(existingProduct);
                }

                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "CategoryId", "CategoryName");
            return View(product); 
        }

        // POST: on model pop up delete via ajax
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var product = _context.products.Find(id);
            if (product != null)
            {
                _context.products.Remove(product);
                _context.SaveChanges();
            }
            return RedirectToAction("Index"); // Redirect to the index page after successful deletion
        }
    }
}