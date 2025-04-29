using Domain.Results;
using MediatR;

namespace Application.Products.Commands.CreateProduct;

public record CreateProductCommand : IRequest<Result<CreatedProductDto>>
{
    public string ProductName { get; set; } = "";

    public Guid? ProductTypeId { get; set; }
}
