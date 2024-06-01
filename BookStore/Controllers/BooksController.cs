using BookStore.Data;
using BookStore.Models;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public BooksController(ApplicationDbContext context, IWebHostEnvironment WebHostEnvironment)
        {
            this.context = context;
            webHostEnvironment = WebHostEnvironment;
        }

        public IActionResult Index()
        {
            var books = context.Books.
                Include(book => book.Author).
                Include(book => book.Categories).
                ThenInclude(book=>book.category).
                ToList();

            //foreach (var item in books)
            //{
            //    Console.WriteLine( $"title...{item.Title}....{item.Author.Name}");
            //    foreach (var item1 in item.Categories)
            //    {
            //        Console.WriteLine( $"{item1.category.Name}");
            //    }
            //}

            var bookVMs = books.Select(book => new BookVM
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author.Name,
                Publisher = book.Publisher,
                PublishDate = book.PublishDate,
                ImageUrl = book.ImageUrl,
                Categories = book.Categories.Select(c=> c.category.Name).ToList(),
            }).ToList();

            //var bookVMs = new List<BookVM>();
            //foreach (var book in books)
            //{
            //    var bookVM = new BookVM
            //    {
            //        Id = book.Id,
            //        Title = book.Title,
            //        Author=book.Author.Name,
            //        Publisher = book.Publisher,
            //        PublishDate = book.PublishDate,
            //        ImageUrl = book.ImageUrl,
            //        Categories = new List<string>(), 
            //    };
            //    foreach (var c in book.Categories)
            //    {
            //        bookVM.Categories.Add(c.category.Name);
            //    }
            //    bookVMs.Add(bookVM);

            //}

            return View(bookVMs);
            
        }

        [HttpGet]
        public IActionResult Create()
        {
            var authors = context.Authors.OrderBy(e => e.Name).ToList();
            var categories = context.Categories.OrderBy(e => e.Name).ToList();


            var authorList = authors.Select(item => new SelectListItem
            {
                Value = item.Id.ToString(),
                Text = item.Name,
            }).ToList();

            var categoryList = categories.Select(item => new SelectListItem
            {
                Value = item.Id.ToString(),
                Text = item.Name,
            }).ToList();

            //var authorList = new List<SelectListItem>();
            //foreach (var item in authors)
            //{
            //    authorList.Add(new SelectListItem
            //    {
            //        Value = item.Id.ToString(),
            //        Text = item.Name,
            //    });
            //}

            //var categoryList = new List<SelectListItem>();
            //foreach (var item in categories)
            //{
            //    categoryList.Add(new SelectListItem
            //    {
            //        Value = item.Id.ToString(),
            //        Text = item.Name,
            //    });
            //}

            var viewModel = new BookFormVM
            {
                Authors = authorList,
                Categories = categoryList,
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(BookFormVM ViewModel)
        {
            if(!ModelState.IsValid)
            {
                return View(ViewModel);
            }

            string ImageName = "";
            if (ViewModel.ImageUrl != null)
            {
                ImageName = Path.GetFileName(ViewModel.ImageUrl.FileName);
                var path = Path.Combine($"{webHostEnvironment.WebRootPath}/image/books", ImageName);
                var stream = System.IO.File.Create(path);
                ViewModel.ImageUrl.CopyTo(stream);
            }

            var book = new Book
            {
                Title = ViewModel.Title,
                AuthorId = ViewModel.AuthorId,
                Description = ViewModel.Description,
                Publisher = ViewModel.Publisher,
                PublishDate = ViewModel.PublishDate,
                ImageUrl = ImageName,
                Categories = ViewModel.SelectedCategories.Select(Id => new BookCategory
                {
                    CategoryId = Id,
                }).ToList()
            };

            try
            {
                context.Books.Add(book);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("Title", "Title name already exist");
                return View("Create", ViewModel);
            }
        }

    }
}
