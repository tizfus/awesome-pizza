using System.Text.Json.Serialization;
using AwesomePizza.Persistence;
using AwesomePizza.Ports.Input;
using AwesomePizza.Ports.Output;
using Microsoft.EntityFrameworkCore;

namespace AwesomePizza.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = CreateWebHostBuilder(args).Build();

        MigrateDatabase(builder);

        builder.Run();
    }

    private static void MigrateDatabase(IHost builder)
    {
        using var scope = builder.Services.CreateScope();
        scope.ServiceProvider.GetRequiredService<Context>().Database.Migrate();
    }

    public static IHostBuilder CreateWebHostBuilder(string[] args)
        => Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());

}

public class Startup(IConfiguration configuration)
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers()
            .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddDbContext<Context>(options => options.UseSqlite(configuration.GetConnectionString("Main")));
        services.AddScoped<IRepositoryOrder, RepositoryOrder>();
        services.AddScoped<IOrderService, Core.OrderService>();
        services.AddScoped<OrderAdapter>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.
        if (!env.IsProduction())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        

        app.UseHttpsRedirection();

        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}
