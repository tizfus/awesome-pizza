
namespace AwesomePizza.Ports;

/// <summary>
/// Encapsulates the return of a method. It's a successful result if the value is not null.
/// </summary>
/// <typeparam name="T"></typeparam>
public record class Result<T> (T? Value)
{
    public bool Succeeded { get; } = Value is not null;


    public Result<T1> Map<T1>(Func<T, T1> map) where T1 : class
    {
        return Succeeded ? new (map(Value!)) : new ((T1?)null);
    }

    public T1 Map<T1>(Func<T, T1> onSuccess, Func<T1> onFailure)
    {
        return Succeeded ? onSuccess(Value!) : onFailure();
    }
}

public static class Result
{
    public static Result<T> Fail<T>() where T : class => new(null);
    public static Result<T> Success<T>(T value) => new(value);
}