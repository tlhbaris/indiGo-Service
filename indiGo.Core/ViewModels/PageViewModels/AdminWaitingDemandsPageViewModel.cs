namespace indiGo.Core.ViewModels.PageViewModels;

public class AdminWaitingDemandsPageViewModel
{
    public List<UserViewModel>? ElectricalTechnics { get; set; } = new List<UserViewModel>();
    public List<UserViewModel>? GasTechnics { get; set; } = new List<UserViewModel>();
    public List<UserViewModel>? PlumbingTechnics { get; set; } = new List<UserViewModel>();
    public List<ServiceDemandViewModel>? ServiceDemands { get; set; } = new List<ServiceDemandViewModel>();
}