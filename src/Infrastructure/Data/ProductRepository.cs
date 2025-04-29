using Application.Repositores;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ProductRepository(ApplicationDbContext db) : IProductRepository
{
    public void Add(Product product)
    {
        db.Products.Add(product);
    }

    public async Task<IEnumerable<Product>> GetAll()
    {
        return await db.Products.Include(x => x.ProductType).ToListAsync();
    }

    public async Task<Product?> GetById(Guid id)
    {
        return await db.Products.Include(x => x.ProductType).SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task SaveChanges()
    {
        await db.SaveChangesAsync();
    }
}
