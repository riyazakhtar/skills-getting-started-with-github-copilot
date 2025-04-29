using Application.Repositores;
using Domain.Entities;
using Domain.Results;
using MediatR;

namespace Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandler(
    IProductRepository productRepository,
    IProductTypeRepository productTypeRepository
) : IRequestHandler<CreateProductCommand, Result<CreatedProductDto>>
{
    public async Task<Result<CreatedProductDto>> Handle(
        CreateProductCommand request,
        CancellationToken cancellationToken
    )
    {
        ProductType? productType = null;
        if (request.ProductTypeId is not null)
        {
            productType = await productTypeRepository.GetById(request.ProductTypeId.Value);
            if (productType is null)
            {
                return new Result<CreatedProductDto>(
                    null,
                    ResultStatus.Error,
                    "Product type not found"
                );
            }
        }
        var product = new Product { ProductName = request.ProductName, ProductType = productType };
        productRepository.Add(product);
        await productRepository.SaveChanges();
        return new Result<CreatedProductDto>(
            new CreatedProductDto { Id = product.Id },
            ResultStatus.Created
        );
    }
}
