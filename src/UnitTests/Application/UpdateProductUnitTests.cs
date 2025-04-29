using Application.Products.Commands.UpdateProduct;
using Application.Repositores;
using Domain.Entities;
using Domain.Results;
using NSubstitute;

namespace UnitTests.Application;

public class UpdateProductUnitTests
{
    [Fact]
    public async void NameUpdated()
    {
        // Arrange
        var productRepository = Substitute.For<IProductRepository>();
        var product = new Product() { ProductName = "Old", ProductType = new() };
        productRepository.GetById(default).Returns(product);
        var productTypeRepository = Substitute.For<IProductTypeRepository>();
        var command = new UpdateProductCommand() { ProductName = "New" };
        var commandHandler = new UpdateProductHandler(productRepository, productTypeRepository);

        // Act
        var result = await commandHandler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(ResultStatus.Ok, result.Status);
        Assert.Equal("New", product.ProductName);
    }
}
