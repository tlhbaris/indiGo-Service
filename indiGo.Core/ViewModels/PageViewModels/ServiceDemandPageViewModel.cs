using Microsoft.AspNetCore.Mvc.Rendering;

namespace indiGo.Core.ViewModels;

public class ServiceDemandPageViewModel
{
    public ServiceDemandViewModel? ServiceDemand { get; set; }
    public List<SelectListItem>? Addresses { get; set; } = new List<SelectListItem>();
}
