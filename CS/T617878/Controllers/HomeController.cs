using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using T617878.Models;

namespace T617878.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(NorthwindContext context)
        {
            NorthwindContext = context;
        }
        protected NorthwindContext NorthwindContext { get; }
        public IActionResult Index()
        {
            MyModel model = new MyModel() { ID = 0, CategoryID = 1, ProductID = 1, Text = "Test" };
            ViewData["Category"] = NorthwindContext.Categories;
            ViewData["Product"] = NorthwindContext.Products.Where(m => m.CategoryID == model.CategoryID);
          return View(model);
        }
        [HttpPost]
        public IActionResult Index(MyModel model)
        {
            ViewData["Category"] = NorthwindContext.Categories;
            ViewData["Product"] = NorthwindContext.Products.Where(m => m.CategoryID == model.CategoryID);
            return View(model);
        }
        public IActionResult CategoryComboBoxPartialView()
        {
            ViewData["Category"] = NorthwindContext.Categories;
            return PartialView();
        }
        public IActionResult ProductComboBoxPartialView(int? Category, int? Product, bool? categoryChanged)
        {
            if (Category != null)
                ViewData["Product"] = NorthwindContext.Products.Where(m => m.CategoryID == Category);
            else
                ViewData["Product"] = NorthwindContext.Products;
            if (categoryChanged == true)
                return PartialView(null);
            else
                return PartialView(Product);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}