using indiGo.Core.Entities.Abstract;

namespace indiGo.Core.Entities;

public class Receipt : BaseEntity<int>
{
    public int DemandId { get; set; }
    public ServiceDemand? Demand { get; set; }
}