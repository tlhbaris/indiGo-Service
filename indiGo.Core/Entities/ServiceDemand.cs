using indiGo.Core.Entities.Abstract;
namespace indiGo.Core.Entities;

public class ServiceDemand : BaseEntity<int>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string TCKN { get; set; }
    public int AddressId { get; set; }
    public string Problem { get; set; }
    public string PhoneNumber { get; set; }
    public string Category { get; set; }
    public string UserId { get; set; }
    public string? ServiceId { get; set; }
    public bool Accepted { get; set; } = false;
    public bool Completed { get; set; } = false;
    public bool Receipted { get; set; } = false;
    public bool Paid { get; set; } = false;
}