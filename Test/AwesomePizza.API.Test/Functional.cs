using System.Net.Http.Json;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc.Testing;

namespace AwesomePizza.API.Test;

public class Functional
{
    private readonly HttpClient httpClient;

    public Functional()
    {
        httpClient = new WebApplicationFactory<Program>().CreateClient();
    }

    [Fact]
    public async void CustomerCanOrderAPizza()
    {
        var (response, jsonContent) = await NewOrderAsync();

        response.EnsureSuccessStatusCode();
        Assert.NotNull(jsonContent);
        Assert.NotNull(jsonContent["id"]);
    }

    private async Task<(HttpResponseMessage, JsonObject)> NewOrderAsync()
    {
        var response = await httpClient.PostAsync("/api/order", null);
        var jsonContent = await response.Content.ReadFromJsonAsync<JsonObject>();
        return (response, jsonContent!);
    }

}