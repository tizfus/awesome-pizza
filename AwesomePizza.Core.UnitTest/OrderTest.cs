using AwesomePizza.Ports;
using AwesomePizza.Ports.Output;
using Moq;

namespace AwesomePizza.Core.UnitTest;

public class OrderTest
{
    [Fact]
    public void CreateAOrder()
    {
        var expectedId = $"{new Random().Next()}";

        var mockRepository = new Mock<IRepository<OrderId>>();
        mockRepository.Setup(mock => mock.Save(It.IsAny<string>())).Returns(new OrderId(expectedId));

        var actual = new Order(mockRepository.Object).New();

        Assert.Equal(expectedId, $"{actual}");
    }
}