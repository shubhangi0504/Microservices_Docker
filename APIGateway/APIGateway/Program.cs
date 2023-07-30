using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();


builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddSwaggerForOcelot(builder.Configuration);
builder.Services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

app.UsePathBase("/gateway");
app.UseStaticFiles();

app.UseSwaggerForOcelotUI(builder.Configuration, opt =>
{
    opt.DownstreamSwaggerEndPointBasePath = "/gateway/swagger/docs";
    opt.PathToSwaggerGenerator = "/swagger/docs";
    //opt.SwaggerEndpoint("/myApi/swagger/v1/swagger.json", "V1 Docs");
});

app.UseCors("corsapp");
await app.UseOcelot();


app.Run();
