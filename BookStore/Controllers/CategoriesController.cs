using Microsoft.AspNetCore.Mvc;
using BookStore.Data;
using BookStore.Models;
using BookStore.ViewModels;

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
            var categories = context.Categories.ToList();
            var categoriesVM = categories.Select(item => new CategoryVM
            {
                Id = item.Id,
                Name = item.Name,
            }).ToList();


            //foreach (var item in categories)
            //{
            //    var categoryVM = new CategoryVM()
            //    {
            //        Id = item.Id,
            //        Name = item.Name,
            //    };
            //    categoriesVM.Add(categoryVM);
            //}



            return View(categoriesVM);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CategoryVM categoryVM)
        {
            if (!ModelState.IsValid)
            {
                return View(categoryVM);
            }

            var category = new Category
            {
                Name = categoryVM.Name,
            };

            try
            {
            context.Categories.Add(category);
            context.SaveChanges();
            return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("Name", "Category name already exist");
                return View(categoryVM);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id) 
        {
            var category = context.Categories.Find(id);

            if (category is null)
            {
                return NotFound();
            }

            var categoryVM = new CategoryVM
            {
                Id = id,
                Name = category.Name,
            };

            return View("Create", categoryVM);
        }

        [HttpPost]
        public IActionResult Edit (CategoryVM categoryVM) 
        {

            if (!ModelState.IsValid)
            {
                return View("Create", categoryVM);
            }

            var category = context.Categories.Find(categoryVM.Id);

            if(category is null)
            {
                return NotFound();
            }
            try
            {
            category.Name = categoryVM.Name;
            category.UpdatedOn = DateTime.Now;
            context.SaveChanges();

            return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("Name", "Category name already exist");
                return View("Create", categoryVM);
            }
        }

        public IActionResult Details(int id) 
        {
            var category = context.Categories.Find(id);

            if (category is null)
            {
                return NotFound();
            }

            var ViewModel = new CategoryVM
            {
                Id = category.Id,
                Name = category.Name,
                UpdatedOn = category.UpdatedOn,
                CreatedOn = category.CreatedOn,
            };

            return View(ViewModel);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var category = context.Categories.Find(id);

            if (category is null)
            {
                return NotFound();
            }

            context.Categories.Remove(category);
            context.SaveChanges();

            //return RedirectToAction("Index");
            return Ok();
        }
        public IActionResult CheckName(CategoryVM categoryVM)
        {
            var isExsist = context.Categories.Any(category =>  category.Name == categoryVM.Name);
            return Json(isExsist);
        }

    }
}
