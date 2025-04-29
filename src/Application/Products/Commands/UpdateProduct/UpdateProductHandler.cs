using Application.Repositores;
using Domain.Entities;
using Domain.Results;
using MediatR;

namespace Application.Products.Commands.UpdateProduct;

public class UpdateProductHandler(
    IProductRepository productRepository,
    IProductTypeRepository productTypeRepository
) : IRequestHandler<UpdateProductCommand, Result>
{
    public async Task<Result> Handle(
        UpdateProductCommand request,
        CancellationToken cancellationToken
    )
    {
        ProductType? productType = null;
        if (request.ProductTypeId is not null)
        {
            productType = await productTypeRepository.GetById(request.ProductTypeId.Value);
            if (productType is null)
            {
                return new Result<UpdateProductCommand>(
                    null,
                    ResultStatus.Error,
                    "Product type not found"
                );
            }
        }
        var product = await productRepository.GetById(request.ProductId);
        if (product is null)
        {
            return new Result(ResultStatus.NotFound, "Product not found");
        }
        product.ProductName = request.ProductName;
        await productRepository.SaveChanges();
        return new Result();
    }
}
