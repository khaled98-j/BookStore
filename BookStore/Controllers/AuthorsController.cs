using BookStore.Data;
using BookStore.Models;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly ApplicationDbContext context;

        public AuthorsController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var authers = context.Authors.ToList();
            var authersVM = authers.Select(item => new AuthorVM
            {
                Id = item.Id,
                Name = item.Name,
            }).ToList();

            //foreach (var item in authers)
            //{
            //    var autherVM = new AuthorVM()
            //    {
            //        Id = item.Id,
            //        Name = item.Name,
            //    };
            //    authersVM.Add(autherVM);
            //}

            return View(authersVM);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Form");
        }

        [HttpPost]
        public IActionResult Create(AuthorVM authorVM)
        {
            if (!ModelState.IsValid)
            {
                return View("Form",authorVM);
            }

            var author = new Author
            {
                Name = authorVM.Name,
            };

            try
            {
                context.Authors.Add(author);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("Name", "Author name already exist");
                return View("Form",authorVM);
            }
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var author = context.Authors.Find(id);

            if (author is null)
            {
                return NotFound();
            }

            var authorVM = new AuthorVM
            {
                Id = id,
                Name = author.Name,
            };

            return View("Form", authorVM);
        }

        [HttpPost]
        public IActionResult Edit(AuthorVM authorVM)
        {

            if (!ModelState.IsValid)
            {
                return View("Form", authorVM);
            }

            var author = context.Authors.Find(authorVM.Id);

            if (author is null)
            {
                return NotFound();
            }
            try
            {
                author.Name = authorVM.Name;
                author.UpdatedOn = DateTime.Now;
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("Name", "author name already exist");
                return View("Form", authorVM);
            }
        }

        public IActionResult Details(int id)
        {
            var author = context.Authors.Find(id);

            if (author is null)
            {
                return NotFound();
            }

            var ViewModel = new AuthorVM
            {
                Id = author.Id,
                Name = author.Name,
                UpdatedOn = author.UpdatedOn,
                CreatedOn = author.CreatedOn,
            };

            return View(ViewModel);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var author = context.Authors.Find(id);

            if (author is null)
            {
                return NotFound();
            }

            context.Authors.Remove(author);
            context.SaveChanges();

            //return RedirectToAction("Index");
            return Ok();
        }
    }
}
