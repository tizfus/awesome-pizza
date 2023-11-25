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

    [Fact]
    public async void CustomerCanViewTheirOrder()
    {
        var (_, json) = await NewOrderAsync();

        var response = await httpClient.GetAsync($"/api/order/{json["id"]}");
        response.EnsureSuccessStatusCode();
        
        var jsonContent = await response.Content.ReadFromJsonAsync<JsonObject>();
        Assert.NotNull(jsonContent);
        Assert.NotNull(jsonContent["id"]);
        Assert.NotNull(jsonContent["status"]);
    }

    [Fact]
    public async void AdminCanViewAllOrders()
    {
        await NewOrderAsync();
        await NewOrderAsync();
        await NewOrderAsync();

        var response = await httpClient.GetAsync($"/api/admin/order");
        response.EnsureSuccessStatusCode();
        
        var jsonArray = await response.Content.ReadFromJsonAsync<JsonArray>();
        Assert.NotEmpty(jsonArray!);
    }

    [Fact]
    public async void AdminCanUpdateTheOrder()
    {
        var (_, json) = await NewOrderAsync();
        
        var response = await httpClient.PutAsync($"/api/admin/order/{json["id"]}",  JsonContent.Create(new { status = "Doing" }));
        response.EnsureSuccessStatusCode();
        
        var jsonContent = await response.Content.ReadFromJsonAsync<JsonObject>();
        Assert.NotNull(jsonContent);
        Assert.NotNull(jsonContent["id"]);
        Assert.Equal("Doing", $"{jsonContent["status"]}");
    }

    private async Task<(HttpResponseMessage, JsonObject)> NewOrderAsync()
    {
        var response = await httpClient.PostAsync("/api/order", null);
        var jsonContent = await response.Content.ReadFromJsonAsync<JsonObject>();
        return (response, jsonContent!);
    }

}