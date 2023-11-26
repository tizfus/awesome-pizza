using AwesomePizza.Ports;

namespace AwesomePizza.API.Models;

public record OrderId(string Id);
public record UpdateRequest(OrderStatus Status);
public record Order(string Id, OrderStatus Status);