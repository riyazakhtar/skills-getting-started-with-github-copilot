using Application.Products.Queries.GetProductById;
using Application.Products.Queries.GetProducts;
using Application.Repositores;
using AutoMapper;
using Domain.Results;
using NSubstitute;

namespace UnitTests.Application;

public class GetProductsUnitTests
{
    [Fact]
    public async void ReturnsOk()
    {
        // Arrange
        var productRepository = Substitute.For<IProductRepository>();
        var mapper = Substitute.For<IMapper>();
        mapper.Map<List<ProductDto>>(default).ReturnsForAnyArgs([new()]);
        var command = new GetProductsQuery();
        var commandHandler = new GetProductsHandler(productRepository, mapper);

        // Act
        var result = await commandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(ResultStatus.Ok, result.Status);
        Assert.NotEmpty(result.Value!);
    }

    [Fact]
    public async void ReturnsNotFound()
    {
        // Arrange
        var productRepository = Substitute.For<IProductRepository>();
        var mapper = Substitute.For<IMapper>();
        var command = new GetProductByIdQuery();
        var commandHandler = new GetProductByIdHandler(productRepository, mapper);

        // Act
        var result = await commandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(ResultStatus.NotFound, result.Status);
    }
}
