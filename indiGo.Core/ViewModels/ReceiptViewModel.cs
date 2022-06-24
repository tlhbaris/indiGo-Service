using indiGo.Core.Entities;

namespace indiGo.Core.ViewModels;

public class ReceiptViewModel
{
    public string DemandId { get; set; }
    public ServiceDemand? Demand { get; set; }
}