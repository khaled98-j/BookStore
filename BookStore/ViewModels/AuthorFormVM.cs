using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BookStore.ViewModels
{
    public class AuthorFormVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "the filed is required")]
        [MaxLength(50, ErrorMessage = "max lenght is 50")]
        public string Name { get; set; }
    }
}
