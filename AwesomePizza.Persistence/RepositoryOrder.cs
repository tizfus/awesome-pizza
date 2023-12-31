﻿using AwesomePizza.Ports;
using AwesomePizza.Ports.Output;

namespace AwesomePizza.Persistence;

public class RepositoryOrder(Context context) : IRepositoryOrder
{

    private readonly Context context = context;

    public bool Exists(OrderId id) => Find(id) is not null;

    public Order Get(OrderId id)
    {
        var order = Find(id)!;
        return ToOrderPort(order);
    }

    public OrderId Save(Order order)
    {
        context.Add(ToEntity(order));
        context.SaveChanges();

        return order.Id;
    }

    public IEnumerable<Order> List()
    {
        return context.Orders
            .Select(ToOrderPort)
            .ToList();
    }

    public void UpdateStatus(OrderId id, OrderStatus status)
    {
        var result = Find(id)!;
        result.Status = $"{status}";

        context.SaveChanges();
    }

    private Entity.Order? Find(OrderId id)
    {
        return context.Orders.Find($"{id}");
    }
    
    private static OrderStatus ToOrderStatus(string status)
    {
        return Enum.Parse<OrderStatus>(status);
    }


    private static Order ToOrderPort(Entity.Order order)
    {
        return new Order(order.Id, ToOrderStatus(order.Status), order.CreatedAt);
    }

    private static Entity.Order ToEntity(Order order)
    {
        return new Entity.Order 
        { 
            Id = $"{order.Id}", 
            Status = $"{order.Status}",
            CreatedAt = order.CreatedAt
        };
    }
}