using System.Text.Json.Serialization;
using Domain.Results;
using MediatR;

namespace Application.Products.Commands.UpdateProduct;

public record UpdateProductCommand : IRequest<Result>
{
    [JsonIgnore]
    public Guid ProductId { get; set; }

    public string ProductName { get; set; } = "";

    public Guid? ProductTypeId { get; set; }
}
