using Disaster_Resource_Allocation_API.Repositories.Interface;
using Disaster_Resource_Allocation_API.Repositories;
using Disaster_Resource_Allocation_API.Services.Interfaces;
using Disaster_Resource_Allocation_API.Services;
using StackExchange.Redis;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var redisConnectionString = configuration.GetConnectionString("Redis");
if (string.IsNullOrEmpty(redisConnectionString))
{
    throw new InvalidOperationException("ERROR : Redis Connection String not found");
}
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    try
    {
        return ConnectionMultiplexer.Connect(redisConnectionString);
    }
    catch (RedisConnectionException ex)
    {
        var logger = sp.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "ERROR : Failed to connect to Redis.");
        throw;
    }
});


builder.Services.AddSingleton<IAreaRepository, AreaRepository>();
builder.Services.AddSingleton<ITruckRepository, TruckRepository>();
builder.Services.AddSingleton<IAssignmentRepository, AssignmentRepository>();


builder.Services.AddScoped<IAreaService, AreaService>();
builder.Services.AddScoped<ITruckService, TruckService>();
builder.Services.AddScoped<IAssignmentService, AssignmentService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Disaster Resource Allocation API",
        Version = "v1",
        Description = "This API is made By Patcharapol Tadfan. For Test Only"
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Disaster Resource Allocation API V1");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();