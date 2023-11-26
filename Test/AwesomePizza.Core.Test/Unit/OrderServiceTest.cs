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
        mockRepository.Setup(mock => mock.Save(It.IsAny<OrderId>(), OrderStatus.Todo)).Returns(new OrderId(expectedId));

        var actual = new OrderService(mockRepository.Object).New();

        Assert.Equal(expectedId, $"{actual}");
    }

    [Fact]
    public void CreateOrderWithUniqId()
    {
        var createdOrderIds = new List<OrderId>();

        var mockRepository = new Mock<IRepositoryOrder>();
        mockRepository.Setup(mock => mock.Save(It.IsAny<OrderId>(), It.IsAny<OrderStatus>()))
            .Callback<OrderId, OrderStatus>((id,_) => createdOrderIds.Add(id))
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
        mockRepository.Setup(mock => mock.Get(It.IsAny<OrderId>())).Returns(new Order("any id 1", default));

        var actual = new OrderService(mockRepository.Object).Get("any id 2");

        Assert.IsType<Order>(actual);
        mockRepository.Verify(mock => mock.Get(It.IsAny<OrderId>()), Times.Once);
    }

    [Fact]
    public void GetOrderList()
    {
        var mockRepository = new Mock<IRepositoryOrder>();
        mockRepository.Setup(mock => mock.List()).Returns([]);

        var actual = new OrderService(mockRepository.Object).List();

        Assert.IsAssignableFrom<IEnumerable<Order>>(actual);
        mockRepository.Verify(mock => mock.List(), Times.Once);
    }

    [Fact]
    public void UpdateStatus()
    {
        var mockRepository = new Mock<IRepositoryOrder>();
        mockRepository.Setup(mock => mock.Save(It.IsAny<OrderId>(), It.IsAny<OrderStatus>()));
        mockRepository.Setup(mock => mock.Get(It.IsAny<OrderId>())).Returns(new Order("any id 1", default));

        var actual = new OrderService(mockRepository.Object).UpdateStatus("any id 2", default);

        Assert.NotNull(actual);
        mockRepository.Verify(mock => mock.Save(It.IsAny<OrderId>(), It.IsAny<OrderStatus>()), Times.Once);
        mockRepository.Verify(mock => mock.Get(It.IsAny<OrderId>()), Times.Once);
    }
}