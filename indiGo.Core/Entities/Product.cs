using indiGo.Core.Entities.Abstract;

namespace indiGo.Core.Entities;

public class Product : BaseEntity<int>
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }

}