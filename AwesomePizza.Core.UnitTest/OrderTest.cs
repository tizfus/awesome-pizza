namespace AwesomePizza.Core.UnitTest;

public class OrderTest
{
    [Fact]
    public void CreateAOrder()
    {
        var order = new Order().New();

        Assert.False(string.IsNullOrEmpty($"{order}"));
    }
}