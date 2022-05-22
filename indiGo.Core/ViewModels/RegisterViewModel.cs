using System.ComponentModel.DataAnnotations;

namespace indiGo.Core.ViewModels;

public class RegisterViewModel
{
    [Required(ErrorMessage = "İsim alanı gereklidir.")]
    [Display(Name = "İsim")]
    [MaxLength(50)]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Soyisim alanı gereklidir.")]
    [Display(Name = "Soyisim")]
    [MaxLength(50)]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Kullanıcı adı alanı gereklidir.")]
    [Display(Name = "Kullanıcı Adı")]
    [MaxLength(15)]
    public string Username { get; set; }

    [Required(ErrorMessage = "Email alanı gereklidir.")]
    [Display(Name = "Email")]
    [EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "Şifre alanı gereklidir.")]
    [Display(Name = "Şifre")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required(ErrorMessage = "Şifre Tekrar alanı gereklidir.")]
    [Display(Name = "Şifre Tekrar")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Şifreler uyuşmuyor!")]
    public string ConfirmPassword { get; set; }


}