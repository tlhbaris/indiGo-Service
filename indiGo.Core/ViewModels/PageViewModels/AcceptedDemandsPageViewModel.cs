namespace indiGo.Core.ViewModels.PageViewModels;

public class AcceptedDemandsPageViewModel
{
    public List<ServiceDemandViewModel>? ServiceDemands { get; set; } = new List<ServiceDemandViewModel>();
    public List<ProductViewModel>? Products { get; set; } = new List<ProductViewModel>();
    public DemandProductReceiptViewModel? DemandProducts { get; set; }
}