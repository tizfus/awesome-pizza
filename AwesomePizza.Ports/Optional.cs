
namespace AwesomePizza.Ports;

/// <summary>
/// Encapsulates the return of a method. It's a successful result if the value is not null.
/// </summary>
/// <typeparam name="T"></typeparam>
public record class Optional<T> (T? Value)
{
    public bool Succeeded { get; } = Value is not null;


    public Optional<T1> Map<T1>(Func<T, T1> map)
    {
        return Succeeded ? new Optional<T1>(map(Value!)) : new (null!);
    }
}

public static class Optional
{
    public static Optional<T> Fail<T>() where T : class => new(null);
    public static Optional<T> Success<T>(T value) => new(value);
}