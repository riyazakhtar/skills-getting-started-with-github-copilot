using Domain.Common;

namespace Domain.Entities;

public class Product : BaseEntity<Guid>
{
    public string ProductName { get; set; } = "";

    public Guid? ProductTypeId { get; set; }

    public ProductType? ProductType { get; set; }
}
