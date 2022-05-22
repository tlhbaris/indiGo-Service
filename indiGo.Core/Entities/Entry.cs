using indiGo.Core.Entities.Abstract;

namespace indiGo.Core.Entities;

public class Entry : BaseEntity<int>
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int ReceiptId { get; set; }
    public Receipt? Receipt { get; set; }
}