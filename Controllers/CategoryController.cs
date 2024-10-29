using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NipamInfotech_MachineTest.data;
using NipamInfotech_MachineTest.Models;

namespace NipamInfotech_MachineTest.Controllers
{
    public class CategoryController : Controller
    {
        private readonly TestDbContext _context;
        public CategoryController(TestDbContext context)
        {

            _context = context;

        }

       // Category List
        public ActionResult Index()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }

        // addEdit method for save and Update in a database 
        //get 
        [HttpGet]
        public IActionResult addOrEdit(int? id)
        {
            Category category = new Category();

            // If id is null,it will create a new category.
            if (id.HasValue)
            {
                category = _context.Categories.Find(id);
                if (category == null)
                {
                    return NotFound();
                }
            }

            return View(category);
        }

        //post:addEdit method for save and Update in a database 
        [HttpPost]
        public IActionResult addEdit(Category category)
        {
            if (ModelState.IsValid)
            {
                if (category.CategoryId == 0)
                {
                   
                    _context.Categories.Add(category);
                }
                else
                {
                    _context.Categories.Update(category);
                }
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // POST: on modal delete 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var category = _context.Categories.Find(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
