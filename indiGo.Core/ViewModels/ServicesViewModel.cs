namespace indiGo.Core.ViewModels;

public class ServicesViewModel
{
    public List<UserViewModel> ElectricalServices { get; set; } = new List<UserViewModel>();
    public List<UserViewModel> GasServices { get; set; } = new List<UserViewModel>();
    public List<UserViewModel> PlumbingServices { get; set; } = new List<UserViewModel>();
}