using Api.Security;
using Application.Products.Commands.CreateProduct;
using Application.Products.Commands.UpdateProduct;
using Application.Products.Queries.GetProductById;
using Application.Products.Queries.GetProducts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController(ISender sender) : ControllerBase
{
    [HttpGet]
    [RequireRole(Role.ProductReadAll, Role.ProductReadWriteAll)]
    [SwaggerOperation(OperationId = "GetListOfProducts", Description = "Get all products")]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetListOfProducts()
    {
        return this.ToActionResult(await sender.Send(new GetProductsQuery()));
    }

    [HttpPost]
    [RequireRole(Role.ProductReadWriteAll)]
    [SwaggerOperation(OperationId = "CreateNewProduct", Description = "Create a new product")]
    public async Task<ActionResult> CreateNewProduct(CreateProductCommand createProductCommand)
    {
        return this.ToActionResult(await sender.Send(createProductCommand));
    }

    [HttpPut("{id}")]
    [RequireRole(Role.ProductReadWriteAll)]
    [SwaggerOperation(OperationId = "UpdateProduct", Description = "Update product")]
    public async Task<ActionResult> UpdateProduct(
        Guid id,
        UpdateProductCommand updateProductCommand
    )
    {
        updateProductCommand.ProductId = id;
        return this.ToActionResult(await sender.Send(updateProductCommand));
    }

    [HttpGet("{id}")]
    [RequireRole(Role.ProductReadWriteAll)]
    [SwaggerOperation(OperationId = "GetProductById", Description = "Get product by id")]
    public async Task<ActionResult<ProductDto>> GetProductById(Guid id)
    {
        return this.ToActionResult(await sender.Send(new GetProductByIdQuery() { ProductId = id }));
    }
}
