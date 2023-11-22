using System.Net.Http.Json;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc.Testing;

namespace AwesomePizza.API.Test;

public class Integration
{
    private readonly HttpClient httpClient;

    public Integration()
    {
        httpClient = new WebApplicationFactory<Program>().CreateClient();
    }

    [Fact]
    public async void CustomerCanOrderAPizza()
    {
        var response = await httpClient.PostAsync("/api/order", null);

        response.EnsureSuccessStatusCode();
        var jsonContent = await response.Content.ReadFromJsonAsync<JsonObject>();
        Assert.NotNull(jsonContent);
        Assert.NotNull(jsonContent["id"]);
    }
}