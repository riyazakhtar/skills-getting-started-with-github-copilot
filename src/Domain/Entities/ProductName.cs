using Domain.Common;

namespace Domain.Entities;

public class ProductName : BaseEntity<Guid>
{
    public string Name { get; set; } = "";
}
