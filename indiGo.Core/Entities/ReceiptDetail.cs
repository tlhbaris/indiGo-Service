using indiGo.Core.Entities.Abstract;

namespace indiGo.Core.Entities;

public class ReceiptDetail:BaseEntity<int>
{
    public int ReceiptId { get; set; }
    public int ProductId { get; set; }
    public decimal Quantity { get; set; }
    public Receipt? Receipt { get; set; }
    public Product? Product { get; set; }
}