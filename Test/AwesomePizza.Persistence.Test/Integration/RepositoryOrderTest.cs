using AwesomePizza.Ports;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace AwesomePizza.Persistence.Test;

public class RepositoryOrderTest : IDisposable
{
    private readonly SqliteConnection connection;
    private readonly Context context;
    private readonly RepositoryOrder repository;

    public RepositoryOrderTest()
    {
        var database = "sqlite.db";
        connection = Connect(database);
        context = Setup();
        repository = new RepositoryOrder(context);
    }

    public void Dispose()
    {
        this.context.Dispose();
        this.connection.Dispose();
    }

    [Fact]
    public void CreateOrder()
    {
        var orderId = $"{new Random().Next()}";
        var actual = repository.Save(orderId, OrderStatus.Todo);
        Assert.Equal(orderId, $"{actual}");
    }

    [Fact]
    public void GetOrder()
    {
        var orderId = $"{new Random().Next()}";
        var status = OrderStatus.Todo;
        repository.Save(orderId, status);
        
        var actual = repository.Get(orderId);

        Assert.Equal(orderId, $"{actual.Id}");
        Assert.Equal(status, actual.Status);
    }

    [Fact]
    public void CreateAndGetWithCorrectStatus()
    {
        foreach (var expected in Enum.GetValues<OrderStatus>())
        {
            var orderId = $"{new Random().Next()}";
            repository.Save(orderId, expected);
            
            var actual = repository.Get(orderId);

            Assert.Equal(expected, actual.Status);
        }
    }

    [Fact]
    public void ListAllOrders()
    {
        var random = new Random();
        repository.Save($"{random.Next()}", OrderStatus.Todo);
        repository.Save($"{random.Next()}", OrderStatus.Todo);
        repository.Save($"{random.Next()}", OrderStatus.Todo);
        repository.Save($"{random.Next()}", OrderStatus.Todo);

        var actual = repository.List();

        Assert.Equal(4, actual.Count());
    }


    private Context Setup()
    {
        var context = new Context(
            new DbContextOptionsBuilder<Context>().UseSqlite(connection).Options
        );
        
        context.Database.EnsureCreated();
        return context;
    }

    private SqliteConnection Connect(string database)
    {
        var connection = new SqliteConnection($"Data Source={database};");
        connection.Open();

        return connection;
    }
}
