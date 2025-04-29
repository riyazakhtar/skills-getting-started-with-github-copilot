using Domain.Entities;

namespace Infrastructure.Data;

public static class TestDataSeeder
{
    private static ProductType productTypeBookcase =
        new() { Id = new("37c8fa0f-af6e-42e9-860c-8188e3d50cd8"), Name = "Bookcase" };

    private static ProductType productTypeDesk =
        new() { Id = new("ba3010c6-e757-4fe5-b74b-f44a5ba1cad6"), Name = "Desk" };

    private static ProductType productTypeSofa =
        new() { Id = new("97905a66-0ecb-4542-b92a-703ca574d81e"), Name = "Sofa" };

    private static Product product1 =
        new()
        {
            Id = new("549fc06f-4343-48c6-9557-a22fff1595c2"),
            ProductName = "BILLY",
            ProductType = productTypeBookcase,
        };

    private static Product product2 =
        new()
        {
            Id = new("c7e25167-af90-4b69-ad2e-82403237abc9"),
            ProductName = "MALM",
            ProductType = productTypeDesk,
        };

    private static Product product3 =
        new()
        {
            Id = new("8e47d14f-5981-4b3a-b75c-8b2b89385ea7"),
            ProductName = "KLIPPAN",
            ProductType = productTypeSofa,
        };

    private static Product[] allProducts = [product1, product2, product3];

    public static void Seed(ApplicationDbContext db)
    {
        foreach (var product in allProducts)
        {
            if (db.Products.Find(product.Id) is null)
            {
                db.Products.Add(product);
            }
        }
        db.SaveChanges();
    }
}
