using Domain.Results;
using MediatR;

namespace Application.Products.Queries.GetProducts;

public record GetProductsQuery : IRequest<Result<IEnumerable<ProductDto>>> { }
