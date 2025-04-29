using Application.Repositores;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ProductTypeRepository(ApplicationDbContext db) : IProductTypeRepository
{
    public void Add(ProductType productType)
    {
        db.ProductTypes.Add(productType);
    }

    public async Task<IEnumerable<ProductType>> GetAll()
    {
        return await db.ProductTypes.ToListAsync();
    }

    public async Task<ProductType?> GetById(Guid id)
    {
        return await db.ProductTypes.SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task SaveChanges()
    {
        await db.SaveChangesAsync();
    }
}
