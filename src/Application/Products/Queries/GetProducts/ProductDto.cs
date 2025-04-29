namespace Application.Products.Queries.GetProducts;

public class ProductDto
{
    public Guid Id { get; set; }
    public string? ProductType { get; set; }
    public string ProductName { get; set; } = "";
}
