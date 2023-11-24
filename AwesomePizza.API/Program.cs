using AwesomePizza.API.Controllers;
using AwesomePizza.Persistence;
using AwesomePizza.Ports;
using AwesomePizza.Ports.Input;
using AwesomePizza.Ports.Output;
using Microsoft.EntityFrameworkCore;

namespace AwesomePizza.API
{

    public partial class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<Context>(options => options.UseSqlite("Data Source=awesome-pizza.db"));
            builder.Services.AddScoped<IRepositoryOrder, RepositoryOrder>();
            builder.Services.AddScoped<IOrder, Core.Order>();
            builder.Services.AddScoped<OrderAdapter>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
            
    }
}