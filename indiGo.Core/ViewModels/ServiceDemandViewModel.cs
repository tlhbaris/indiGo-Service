using System.ComponentModel.DataAnnotations;
using indiGo.Core.Entities;

namespace indiGo.Core.ViewModels;

public class ServiceDemandViewModel
{
    public int Id { get; set; }

    [Display(Name = "Ad")]
    [Required]
    public string FirstName { get; set; }

    [Display(Name = "Soyad")]
    [Required]
    public string LastName { get; set; }

    [Display(Name = "TC Kimlik No")]
    [Required]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "Lütfen geçerli bir TC Kimlik No giriniz.")]
    public string TCKN { get; set; }

    [Display(Name = "Adres")]
    [Required(ErrorMessage = "Lütfen geçerli bir adres seçiniz.")]
    public int AddressId { get; set; }

    [Display(Name = "Telefon Numarası")]
    [Required]
    [Phone]
    public string PhoneNumber { get; set; }

    [Display(Name = "Probleminiz nedir?")]
    [Required]
    public string Problem { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string? Category { get; set; }
    public Address? Address { get; set; }
    public UserViewModel? Service { get; set; }
    public string? ServiceId { get; set; }
    public bool Accepted { get; set; } = false;
    public bool Completed { get; set; } = false;
    public bool Receipted { get; set; } = false;
    public bool Paid { get; set; } = false;

}