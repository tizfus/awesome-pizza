using AwesomePizza.Ports;
using AwesomePizza.Ports.Output;
using Moq;

namespace AwesomePizza.Core.Test.Unit;

public class OrderServiceTest
{
    private readonly Mock<IRepositoryOrder> mockRepository;
    private readonly OrderService service;

    public OrderServiceTest()
    {
        mockRepository = new Mock<IRepositoryOrder>(MockBehavior.Strict);
        service = new OrderService(mockRepository.Object);
    }

    [Fact]
    public void CreateAOrder()
    {
        var expectedId = $"{new Random().Next()}";

        mockRepository.Setup(mock => mock.Save(It.Is<Order>(order => order.Status == OrderStatus.Todo && order.CreatedAt != default)))
            .Returns(new OrderId(expectedId));

        var actual = service.New();

        Assert.Equal(expectedId, $"{actual}");
    }

    [Fact]
    public void CreateOrderWithUniqId()
    {
        var createdOrderIds = new List<OrderId>();

        mockRepository.Setup(mock => mock.Save(It.IsNotNull<Order>()))
            .Callback<Order>(order => createdOrderIds.Add(order.Id))
            .Returns(new OrderId("any"));

        var order = service;

        for (int index = 0; index < 100; index++)
        {
            order.New(); 
        }

        Assert.Equal(createdOrderIds.Count, createdOrderIds.Distinct().Count());
    }

    [Fact]
    public void GetOrder()
    {
        mockRepository.Setup(mock => mock.Exists(It.IsAny<OrderId>())).Returns(true);
        mockRepository.Setup(mock => mock.Get(It.IsAny<OrderId>())).Returns(new Order("any id 1", default, DateTime.Now));

        var actual = service.Get("any id 2");

        Assert.True(actual.Succeeded);
        mockRepository.Verify(mock => mock.Get(It.IsAny<OrderId>()), Times.Once);
    }

    [Fact]
    public void GetNotExistentOrder()
    {
        mockRepository.Setup(mock => mock.Exists(It.IsAny<OrderId>())).Returns(false);

        var actual = service.Get("any id 2");

        Assert.False(actual.Succeeded);
        mockRepository.Verify(mock => mock.Get(It.IsAny<OrderId>()), Times.Never);
    }

    [Fact]
    public void PendingOrder()
    {
        mockRepository.Setup(mock => mock.List()).Returns([
            new Order("any id 1", OrderStatus.Doing, DateTime.Now),
            new Order("any id 2", OrderStatus.Done, DateTime.Now),
            new Order("any id 3", OrderStatus.Todo, DateTime.Now),
        ]);

        var actual = service.Pending();

        Assert.Equal(2, actual.Count());
        Assert.DoesNotContain(actual, order => order.Id == "any id 2");
    }

    [Fact]
    public void PendingOrderAsc()
    {
        mockRepository.Setup(mock => mock.List()).Returns([
            new Order("any id 2", OrderStatus.Todo, new DateTime(2023, 05, 06)),
            new Order("any id 1", OrderStatus.Doing, new DateTime(1990, 1, 1)),
            new Order("any id 3", OrderStatus.Todo, new DateTime(2023, 01, 28)),
        ]);

        var actual = service.Pending();

        Assert.Equal("any id 1", actual.ElementAt(0).Id);
        Assert.Equal("any id 3", actual.ElementAt(1).Id);
        Assert.Equal("any id 2", actual.ElementAt(2).Id);
    }

    [Fact]
    public void UpdateStatus()
    {
        mockRepository.Setup(mock => mock.Exists(It.IsNotNull<OrderId>())).Returns(true);
        mockRepository.Setup(mock => mock.UpdateStatus(It.IsAny<OrderId>(), It.IsAny<OrderStatus>()));
        mockRepository.Setup(mock => mock.Get(It.IsAny<OrderId>())).Returns(new Order("any id 1", default, DateTime.Now));

        var actual = service.UpdateStatus("any id 2", default);

        Assert.NotNull(actual);
        mockRepository.Verify(mock => mock.UpdateStatus(It.IsAny<OrderId>(), It.IsAny<OrderStatus>()), Times.Once);
        mockRepository.Verify(mock => mock.Get(It.IsAny<OrderId>()), Times.Once);
    }

    [Fact]
    public void UpdateStatusNotExistentOrder()
    {
        mockRepository.Setup(mock => mock.Exists(It.IsNotNull<OrderId>())).Returns(false);

        var actual = service.UpdateStatus("any id 2", default);

        Assert.NotNull(actual);
        mockRepository.Verify(mock => mock.Save(It.IsNotNull<Order>()), Times.Never);
    }
}