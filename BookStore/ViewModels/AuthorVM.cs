using System.ComponentModel.DataAnnotations;

namespace BookStore.ViewModels
{
    public class AuthorVM
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
    }
}
