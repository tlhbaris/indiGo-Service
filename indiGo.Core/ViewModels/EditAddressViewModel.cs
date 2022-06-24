using System.ComponentModel.DataAnnotations;

namespace indiGo.Core.ViewModels;

public class EditAddressViewModel
{
    public int Id { get; set; }

    [Display(Name = "Adres Adı")]
    public string AddressName { get; set; }

    [Display(Name = "Şehir")]
    public string City { get; set; }

    [Display(Name = "İlçe")]
    public string District { get; set; }

    [Display(Name = "Mahalle")]
    public string Neighborhood { get; set; }

    [Display(Name = "Sokak/Cadde")]
    public string Street { get; set; }

    [Display(Name = "Apartman No")]
    public string ApartmentNo { get; set; }

    [Display(Name = "Daire No")]
    public int FlatNo { get; set; }

    [Display(Name = "Adres Tarifi")]
    public string AddressInfo { get; set; }
}