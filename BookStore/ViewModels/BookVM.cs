using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BookStore.ViewModels
{
    public class BookVM
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Author { get; set; }
        public string Description { get; set; } = null!;
        public string Publisher { get; set; } = null!;
        [Display(Name = "Publish Date")]
        public DateTime PublishDate { get; set; }
        public string? ImageUrl { get; set; }
        public List<string> Categories { get; set; }
    }
}
