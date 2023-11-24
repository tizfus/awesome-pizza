namespace AwesomePizza.Ports.Output;

public record OrderId(string Value)
{
    public static implicit operator OrderId(string value) => new(value);
    public override string ToString() => Value;
};

