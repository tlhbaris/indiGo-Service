using indiGo.Core.Entities;

namespace indiGo.Core.ViewModels;

public class AddressPageViewModel
{
    public AddressViewModel? AddressViewModel { get; set; }
    public EditAddressViewModel? EditAddressViewModel { get; set; }
    public List<Address>? Addresses { get; set; }
}