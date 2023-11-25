namespace AwesomePizza.API;

public record OrderId(string Id);
public record Order(string Id, OrderStatus Status);

public enum OrderStatus
{
    Todo,
    Doing,
    Done
}