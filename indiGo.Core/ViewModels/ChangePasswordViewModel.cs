using System.ComponentModel.DataAnnotations;

namespace indiGo.Core.ViewModels;

public class ChangePasswordViewModel
{
    [DataType(DataType.Password)]
    [Display(Name ="Mevcut Şifre")]
    [Required(ErrorMessage = "Mevcut şifre alanı gereklidir.")]
    public string CurrentPassword { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Yeni Şifre")]
    [Required(ErrorMessage = "Yeni şifre alanı gereklidir.")]
    public string NewPassword { get; set; }

    [DataType(DataType.Password)]
    [Compare(nameof(NewPassword))]
    [Display(Name = "Yeni Şifre Tekrar")]
    [Required(ErrorMessage = "Yeni şifre tekrar alanı gereklidir.")]
    public string NewPasswordConfirm { get; set; }
}