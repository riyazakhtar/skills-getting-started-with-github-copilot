using Application.Products.Queries.GetProducts;
using Domain.Results;
using MediatR;

namespace Application.Products.Queries.GetProductById;

public record GetProductByIdQuery : IRequest<Result<ProductDto>>
{
    public Guid ProductId { get; set; }
}
