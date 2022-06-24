using System.ComponentModel.DataAnnotations;

namespace indiGo.Core.ViewModels;

public class UserViewModel
{
    [Required]
    [Display(Name = "Ad")]
    [MaxLength(50)]
    public string FirstName { get; set; }

    [Required]
    [Display(Name = "Soyad")]
    [MaxLength(50)]
    public string LastName { get; set; }
    public DateTime RegisterDate { get; set; }

    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Phone]
    [Display(Name = "Telefon Numarası")]
    public string? PhoneNumber { get; set; }

    public string? Id { get; set; }
}