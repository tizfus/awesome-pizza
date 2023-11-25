using AwesomePizza.Ports;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace AwesomePizza.Persistence.Test;

public class RepositoryOrderTest : IDisposable
{
    private readonly SqliteConnection connection;
    private readonly Context context;

    public RepositoryOrderTest()
    {
        var database = "sqlite.db";
        CleanDatabase(database);
        connection = Connect(database);
        context = Setup();
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
        var actual = new RepositoryOrder(context).Save(orderId, OrderStatus.Todo);
        Assert.Equal(orderId, $"{actual}");
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

    private void CleanDatabase(string database)
    {
        File.Delete($"./{database}");
    }
}
