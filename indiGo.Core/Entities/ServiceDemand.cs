using indiGo.Core.Entities.Abstract;

namespace indiGo.Core.Entities;

public class ServiceDemand : BaseEntity<int>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string TCKN { get; set; }
    public string Address { get; set; }
    public string Problem { get; set; }
    public string PhoneNumber { get; set; }
    public int Category { get; set; }

}