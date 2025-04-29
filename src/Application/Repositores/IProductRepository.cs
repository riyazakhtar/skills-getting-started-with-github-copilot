using Domain.Entities;

namespace Application.Repositores;

public interface IProductRepository
{
    void Add(Product product);
    Task<IEnumerable<Product>> GetAll();
    Task<Product?> GetById(Guid id);
    Task SaveChanges();
}
