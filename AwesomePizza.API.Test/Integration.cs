using System.Net;
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
    public async void AppIsRunning()
    {
        var response = await httpClient.GetAsync("/swagger");

        response.EnsureSuccessStatusCode();
    }
}