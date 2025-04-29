using Application.Repositores;
using AutoMapper;
using Domain.Results;
using MediatR;

namespace Application.Products.Queries.GetProducts;

public class GetProductsHandler(IProductRepository productRepository, IMapper mapper)
    : IRequestHandler<GetProductsQuery, Result<IEnumerable<ProductDto>>>
{
    public async Task<Result<IEnumerable<ProductDto>>> Handle(
        GetProductsQuery request,
        CancellationToken cancellationToken
    )
    {
        var products = await productRepository.GetAll();
        return mapper.Map<List<ProductDto>>(products);
    }
}
