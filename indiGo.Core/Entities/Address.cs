using indiGo.Core.Entities.Abstract;

namespace indiGo.Core.Entities;

public class Address:BaseEntity<int>
{
    public string AddressName { get; set; }
    public string City { get; set; }
    public string District { get; set; }
    public string Neighborhood { get; set; }
    public string Street { get; set; }
    public string ApartmentNo { get; set; }
    public int FlatNo { get; set; }
    public string AddressInfo { get; set; }
    public string UserId { get; set; }
}