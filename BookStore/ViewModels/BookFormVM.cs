using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BookStore.ViewModels
{
    public class BookFormVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "the filed is required")]
        [MaxLength(30, ErrorMessage = "max lenght is 30")]
        public string Title { get; set; } = null!;
        [Required(ErrorMessage = "the filed is required")]
        [Display(Name = "Author")]
        public int AuthorId { get; set; }
        public List<SelectListItem>? Authors { get; set; }
        [Required(ErrorMessage = "the filed is required")]
        [MaxLength(100, ErrorMessage = "max lenght is 100")]
        public string Description { get; set; } = null!;
        [Required(ErrorMessage = "the filed is required")]
        [MaxLength(30, ErrorMessage = "max lenght is 30")]
        public string Publisher { get; set; } = null!;
        [Display(Name = "Publish Date")]
        public DateTime PublishDate { get; set; } = DateTime.Now;
        [Display(Name = "choose your image")]
        public IFormFile? ImageUrl { get; set; }
        [Required(ErrorMessage = "the filed is required")]
        [Display(Name = "Category")]
        public List<int> SelectedCategories { get; set; } = new List<int>();
        public List<SelectListItem>? Categories { get; set; }

    }
}
