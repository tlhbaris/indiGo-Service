using indiGo.Core.Entities;

namespace indiGo.Core.ViewModels.PageViewModels;

public class ProductPageViewModel
{
    public List<Product>? Products { get; set; }
    public ProductViewModel? Product { get; set; }
}