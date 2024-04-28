using SampleApp.API.Exceptions.Handlers;
using SampleApp.Application;
using SampleApp.Infrastructure;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var sqlConnectionString = builder.Configuration.GetConnectionString("Default");

if (string.IsNullOrWhiteSpace(sqlConnectionString))
{
    throw new InvalidOperationException("No SQL connection string configured. Please configure `ConnectionStrings:Default`.");
}

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddInfrastructureServices(sqlConnectionString, builder.Environment.IsDevelopment());
builder.Services.AddApplicationServices();

builder.Services.AddExceptionHandler<ValidationExceptionHandler>();
builder.Services.AddExceptionHandler<BadRequestExceptionHandler>();
builder.Services.AddExceptionHandler<FallbackExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseExceptionHandler();

app.Run();
