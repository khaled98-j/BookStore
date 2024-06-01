using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    [Index(nameof(Title), IsUnique = true)]
    public class Book
    {
        public int Id { get; set; }
        [MaxLength(30)]
        public string Title { get; set; } = null!;
        [MaxLength(100)]
        public string Description { get; set; } = null!;
        [MaxLength(30)]
        public string Publisher { get; set; } = null!;
        public DateTime PublishDate { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime UpdatedOn { get; set; } = DateTime.Now;

        public int AuthorId { get; set; }
        public Author Author { get; set; }

        public List<BookCategory> Categories { get; set; } = new List<BookCategory>();

    }
}
