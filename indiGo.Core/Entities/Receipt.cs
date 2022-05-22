using indiGo.Core.Entities.Abstract;

namespace indiGo.Core.Entities;

public class Receipt : BaseEntity<int>
{
    public List<Entry> ReceiptEntries { get; set; }
    public string Operation { get; set; }
}