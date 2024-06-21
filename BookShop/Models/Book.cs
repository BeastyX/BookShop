using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShop.Models;

public class Book
{
    public int Id { get; set; }


    [Required(ErrorMessage = "Naslov je obavezan!")]
    [MaxLength(250)]
    [DisplayName("Naslov")]
    public string Title { get; set; } = string.Empty;

    [DisplayName("Opis knjige")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Autor je obavezan!")]
    [DisplayName("Autor")]
    public string Author { get; set; } = string.Empty;

    [Required(ErrorMessage = "Cena je obavezna!")]
    [DisplayName("Cena")]
    [Range(1, 100000, ErrorMessage = "Cena mora biti u opsegu 1-100.000 din")]
    public double Price { get; set; }


    [Required(ErrorMessage = "Kategorija je obavezna!")]
    [Range(1, int.MaxValue, ErrorMessage = "Kategorija je obavezna!")]
    public int CategoryId { get; set; }

    [ForeignKey(nameof(CategoryId))]
    [Required(ErrorMessage = "Kategorija je obavezna!")]
    [DisplayName("Kategorija")]
    [ValidateNever]
    public Category? Category { get; set; }

    [Required(ErrorMessage = "Slika je obavezna!")]
    [DisplayName("Naslovna slika")]
    [ValidateNever]
    public string CoverImagePath { get; set; } = string.Empty;
}
