using Domain.Entities;

namespace Application.Repositores;

public interface IProductTypeRepository
{
    void Add(ProductType product);
    Task<IEnumerable<ProductType>> GetAll();
    Task<ProductType?> GetById(Guid id);
    Task SaveChanges();
}
