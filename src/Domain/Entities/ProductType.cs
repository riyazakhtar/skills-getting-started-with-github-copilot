using Domain.Common;

namespace Domain.Entities;

public class ProductType : BaseEntity<Guid>
{
    public string Name { get; set; } = "";
}
