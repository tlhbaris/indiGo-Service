using System.ComponentModel.DataAnnotations;

namespace indiGo.Core.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Kullanıcı adı alanı gereklidir.")]
    [Display(Name = "Kullanıcı Adı")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Şifre alanı gereklidir.")]
    [Display(Name = "Şifre")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

}