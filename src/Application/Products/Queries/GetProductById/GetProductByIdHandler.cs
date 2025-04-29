using Application.Products.Queries.GetProducts;
using Application.Repositores;
using AutoMapper;
using Domain.Results;
using MediatR;

namespace Application.Products.Queries.GetProductById;

public class GetProductByIdHandler(IProductRepository productRepository, IMapper mapper)
    : IRequestHandler<GetProductByIdQuery, Result<ProductDto>>
{
    public async Task<Result<ProductDto>> Handle(
        GetProductByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        var product = await productRepository.GetById(request.ProductId);
        if (product is null)
        {
            return new Result<ProductDto>(null, ResultStatus.NotFound, "The product not found.");
        }
        return mapper.Map<ProductDto>(product);
    }
}
