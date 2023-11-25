namespace AwesomePizza.Ports;

public record OrderId(string value)
{
    private readonly string value = value;

    public static implicit operator OrderId(string value) => new(value);
    public override string ToString() => value;
};

