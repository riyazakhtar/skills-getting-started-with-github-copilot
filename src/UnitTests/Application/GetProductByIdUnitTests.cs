using Application.Products.Queries.GetProductById;
using Application.Repositores;
using AutoMapper;
using Domain.Entities;
using Domain.Results;
using NSubstitute;

namespace UnitTests.Application;

public class GetProductByIdUnitTests
{
    [Fact]
    public async void ReturnsOk()
    {
        // Arrange
        var productRepository = Substitute.For<IProductRepository>();
        productRepository.GetById(default).Returns(new Product());
        var mapper = Substitute.For<IMapper>();
        var command = new GetProductByIdQuery();
        var commandHandler = new GetProductByIdHandler(productRepository, mapper);

        // Act
        var result = await commandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(ResultStatus.Ok, result.Status);
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
