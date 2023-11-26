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

        mockRepository.Setup(mock => mock.Save(It.Is<Order>(order => order.Status == OrderStatus.Todo))).Returns(new OrderId(expectedId));

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
        mockRepository.Setup(mock => mock.Get(It.IsAny<OrderId>())).Returns(new Order("any id 1", default));

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
            new Order("any id 1", OrderStatus.Doing),
            new Order("any id 2", OrderStatus.Done),
            new Order("any id 3", OrderStatus.Todo),
        ]);

        var actual = service.Pending();

        Assert.Equal(2, actual.Count());
        Assert.DoesNotContain(actual, order => order.Id == "any id 2");
    }

    [Fact]
    public void Update()
    {
        mockRepository.Setup(mock => mock.Exists(It.IsNotNull<OrderId>())).Returns(true);
        mockRepository.Setup(mock => mock.Save(It.IsNotNull<Order>())).Returns("any id");
        mockRepository.Setup(mock => mock.Get(It.IsAny<OrderId>())).Returns(new Order("any id 1", default));

        var actual = service.Update(new Order("any id 2", default));

        Assert.NotNull(actual);
        mockRepository.Verify(mock => mock.Save(It.IsNotNull<Order>()), Times.Once);
        mockRepository.Verify(mock => mock.Get(It.IsAny<OrderId>()), Times.Once);
    }

    [Fact]
    public void UpdateNotExistentOrder()
    {
        mockRepository.Setup(mock => mock.Exists(It.IsNotNull<OrderId>())).Returns(false);

        var actual = service.Update(new Order("any id 2", default));

        Assert.NotNull(actual);
        mockRepository.Verify(mock => mock.Save(It.IsNotNull<Order>()), Times.Never);
    }
}