using System.ComponentModel.DataAnnotations;

namespace indiGo.Core.ViewModels;

public class ProductViewModel
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "İsim")]
    public string Name { get; set; }

    [Required]
    [Display(Name = "Fiyat")]
    public decimal Price { get; set; }

    [Required]
    [Display(Name="Kategori")]
    public string Category { get; set; }
}