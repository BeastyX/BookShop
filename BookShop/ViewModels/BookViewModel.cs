using BookShop.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace BookShop.ViewModels
{
    public class BookViewModel
    {
        public required Book Book { get; set; }

        [ValidateNever]
        public required IEnumerable<SelectListItem> Categories { get; set; } = [];

        //[Required(ErrorMessage = "Slika je obavezna!")]
        [DisplayName("Naslovna slika")]
        public IFormFile? CoverImage { get; set; }
    }
}