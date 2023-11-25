using AwesomePizza.Ports;
using AwesomePizza.Ports.Output;
using Moq;

namespace AwesomePizza.Core.Test.Unit;

public class OrderServiceTest
{
    [Fact]
    public void CreateAOrder()
    {
        var expectedId = $"{new Random().Next()}";

        var mockRepository = new Mock<IRepositoryOrder>(MockBehavior.Strict);
        mockRepository.Setup(mock => mock.Save(It.IsAny<string>(), OrderStatus.Todo)).Returns(new OrderId(expectedId));

        var actual = new OrderService(mockRepository.Object).New();

        Assert.Equal(expectedId, $"{actual}");
    }

    [Fact]
    public void CreateOrderWithUniqId()
    {
        var createdOrderIds = new List<string>();

        var mockRepository = new Mock<IRepositoryOrder>();
        mockRepository.Setup(mock => mock.Save(It.IsAny<string>(), It.IsAny<OrderStatus>()))
            .Callback<string, OrderStatus>((id,_) => createdOrderIds.Add(id))
            .Returns(new OrderId("any"));

        var order = new OrderService(mockRepository.Object);

        for (int index = 0; index < 100; index++)
        {
            order.New(); 
        }

        Assert.Equal(createdOrderIds.Count, createdOrderIds.Distinct().Count());
    }

    [Fact]
    public void GetOrderDetail()
    {
        var mockRepository = new Mock<IRepositoryOrder>();
        mockRepository.Setup(mock => mock.Get(It.IsAny<string>())).Returns(new OrderDetails("any id 1", default));

        var actual = new OrderService(mockRepository.Object).Get("any id 2");

        Assert.IsType<OrderDetails>(actual);
        mockRepository.Verify(mock => mock.Get(It.IsAny<string>()), Times.Once);
    }
}