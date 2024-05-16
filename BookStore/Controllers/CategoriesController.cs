using Microsoft.AspNetCore.Mvc;
using BookStore.Data;

namespace BookStore.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext context;

        public CategoriesController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetAll()
        {
            var categories = context.Categories.ToList();
            return View(categories);
        }

    }
}
