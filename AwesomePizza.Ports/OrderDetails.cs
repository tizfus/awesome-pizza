﻿namespace AwesomePizza.Ports;

public record OrderDetails(string Id, OrderStatus Status)
{
    public OrderStatus Status { get; set; } = Status;
};
