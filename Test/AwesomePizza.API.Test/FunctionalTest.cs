using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
using AwesomePizza.Persistence;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AwesomePizza.API.Test;

[Collection("Non-Parallel Collection")]
public class FunctionalTest : IDisposable
{
    private readonly HttpClient httpClient;
    private readonly DbContext dbContext;

    public FunctionalTest()
    {
        var factory = new WebApplicationFactory<Program>();
        dbContext = factory.Services.CreateScope().ServiceProvider.GetService<Context>()!;
        httpClient = factory.CreateClient();
    }

    public void Dispose()
    {
        dbContext.Database.EnsureDeleted();
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
        Assert.NotNull(jsonContent["created_at"]);
    }

    [Fact]
    public async void CustomerReceives404WhenViewANotExistentOrder()
    {
        var response = await httpClient.GetAsync($"/api/order/wrong_id");
        
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
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
    public async void AdminCanUpdateAOrder()
    {
        var (_, json) = await NewOrderAsync();
        
        var response = await httpClient.PutAsync($"/api/admin/order/{json["id"]}",  JsonContent.Create(new { status = "Doing" }));
        response.EnsureSuccessStatusCode();
        
        var jsonContent = await response.Content.ReadFromJsonAsync<JsonObject>();
        Assert.NotNull(jsonContent);
        Assert.NotNull(jsonContent["id"]);
        Assert.Equal("Doing", $"{jsonContent["status"]}");
    }

    [Fact]
    public async void AdminReceives404WhenUpdateANonExistentOrder()
    {       
        var response = await httpClient.PutAsync($"/api/admin/order/wrong_id",  JsonContent.Create(new { status = "Doing" }));
        
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }


    private async Task<(HttpResponseMessage, JsonObject)> NewOrderAsync()
    {
        var response = await httpClient.PostAsync("/api/order", null);
        var jsonContent = await response.Content.ReadFromJsonAsync<JsonObject>();
        return (response, jsonContent!);
    }
}

[CollectionDefinition("Non-Parallel", DisableParallelization = true)]
public class TestClassNonParallelCollectionDefinition { }