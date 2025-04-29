using Application.Products.Commands.CreateProduct;
using Application.Repositores;
using Domain.Results;
using NSubstitute;

namespace UnitTests.Application;

public class CreateProductUnitTests
{
    [Fact]
    public async void NotEmptyName_ReturnsCreated()
    {
        // Arrange
        var productRepository = Substitute.For<IProductRepository>();
        var productTypeRepository = Substitute.For<IProductTypeRepository>();
        var command = new CreateProductCommand();
        var commandHandler = new CreateProductCommandHandler(
            productRepository,
            productTypeRepository
        );

        // Act
        var result = await commandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(ResultStatus.Created, result.Status);
    }
}
