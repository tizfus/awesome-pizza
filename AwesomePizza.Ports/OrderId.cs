namespace AwesomePizza.Ports;

public record OrderId(string Value)
{
    public static implicit operator OrderId(string value) => new(value);
    public override string ToString() => Value;
};

