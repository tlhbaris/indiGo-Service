namespace indiGo.Core.ViewModels.PageViewModels;

public class CompletedDemandsPageViewModel
{
    public ServiceDemandViewModel ServiceDemand { get; set; }
    public List<ReceiptProductViewModel> Products { get; set; } = new List<ReceiptProductViewModel>();
    public decimal TotalPrice { get; set; } = 0;
}