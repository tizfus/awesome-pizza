
namespace AwesomePizza.Core;

public record OrderId(string Value)
{
    public static implicit operator OrderId(string value) => new(value);
};


public class Order
{
    public OrderId New()
    {
        return "aaa";
    }
}
