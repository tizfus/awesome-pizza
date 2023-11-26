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
        mockRepository.Setup(mock => mock.Save(It.Is<Order>(order => order.Status == OrderStatus.Todo))).Returns(new OrderId(expectedId));

        var actual = new OrderService(mockRepository.Object).New();

        Assert.Equal(expectedId, $"{actual}");
    }

    [Fact]
    public void CreateOrderWithUniqId()
    {
        var createdOrderIds = new List<OrderId>();

        var mockRepository = new Mock<IRepositoryOrder>();
        mockRepository.Setup(mock => mock.Save(It.IsNotNull<Order>()))
            .Callback<Order>(order => createdOrderIds.Add(order.Id))
            .Returns(new OrderId("any"));

        var order = new OrderService(mockRepository.Object);

        for (int index = 0; index < 100; index++)
        {
            order.New(); 
        }

        Assert.Equal(createdOrderIds.Count, createdOrderIds.Distinct().Count());
    }

    [Fact]
    public void GetOrder()
    {
        var mockRepository = new Mock<IRepositoryOrder>();
        mockRepository.Setup(mock => mock.Exists(It.IsAny<OrderId>())).Returns(true);
        mockRepository.Setup(mock => mock.Get(It.IsAny<OrderId>())).Returns(new Order("any id 1", default));

        var actual = new OrderService(mockRepository.Object).Get("any id 2");

        Assert.True(actual.Succeeded);
        mockRepository.Verify(mock => mock.Get(It.IsAny<OrderId>()), Times.Once);
    }

    [Fact]
    public void GetNotExistentOrder()
    {
        var mockRepository = new Mock<IRepositoryOrder>();
        mockRepository.Setup(mock => mock.Exists(It.IsAny<OrderId>())).Returns(false);

        var actual = new OrderService(mockRepository.Object).Get("any id 2");

        Assert.False(actual.Succeeded);
        mockRepository.Verify(mock => mock.Get(It.IsAny<OrderId>()), Times.Never);
    }

    [Fact]
    public void PendingOrder()
    {
        var mockRepository = new Mock<IRepositoryOrder>();
        mockRepository.Setup(mock => mock.List()).Returns([
            new Order("any id 1", OrderStatus.Doing),
            new Order("any id 2", OrderStatus.Done),
            new Order("any id 3", OrderStatus.Todo),
        ]);

        var actual = new OrderService(mockRepository.Object).Pending();

        Assert.Equal(2, actual.Count());
        Assert.DoesNotContain(actual, order => order.Id == "any id 2");
    }

    [Fact]
    public void UpdateStatus()
    {
        var mockRepository = new Mock<IRepositoryOrder>();
        mockRepository.Setup(mock => mock.Save(It.IsNotNull<Order>()));
        mockRepository.Setup(mock => mock.Get(It.IsAny<OrderId>())).Returns(new Order("any id 1", default));

        var actual = new OrderService(mockRepository.Object).Update(new Order("any id 2", default));

        Assert.NotNull(actual);
        mockRepository.Verify(mock => mock.Save(It.IsNotNull<Order>()), Times.Once);
        mockRepository.Verify(mock => mock.Get(It.IsAny<OrderId>()), Times.Once);
    }
}