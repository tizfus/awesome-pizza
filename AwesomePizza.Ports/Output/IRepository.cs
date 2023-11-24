namespace AwesomePizza.Ports.Output;

public interface IRepository<T> where T : class
{
    OrderId Save(string id);
}
