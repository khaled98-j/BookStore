using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BookStore.ViewModels
{
    public class CategoryVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="the filed is required")]
        [MaxLength(30,ErrorMessage ="max lenght is 30")]
        [Remote("CheckName",null,ErrorMessage ="exists")]
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
    }
}
