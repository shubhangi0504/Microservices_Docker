using Microsoft.EntityFrameworkCore;
using ProductService.Data;
namespace ProductService
{
    class Program
    {
        public static void Main(string[] args)
        {
            // Application logic goes here
            
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();

        builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
        {
            builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
        }));
        // Add services to the container.
        builder.Services.AddDbContext<ProductDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("API_Connection")));

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

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
        app.UseCors();
        app.Run();

        }
    }
}

