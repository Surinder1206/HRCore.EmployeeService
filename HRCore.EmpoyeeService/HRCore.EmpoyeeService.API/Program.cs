using Asp.Versioning;
using HRCore.EmpoyeeService.API.Infrastructure;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEmployeeDbContext(builder.Configuration);
builder.Services.AddEmployeeServices();

// API Versioning
builder.Services.AddApiVersioning(x =>
{
    x.DefaultApiVersion = new ApiVersion(1.0);
    x.AssumeDefaultVersionWhenUnspecified = true;
    x.ReportApiVersions = true;
}).AddMvc();

builder.Services.AddControllers();

// OpenAPI Documentation
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Info = new()
        {
            Title = "Employee Service API",
            Version = context.DocumentName,
            Description = "Provides a set of APIs for managing employee profiles, employment details, and roles within the BrightHR platform."
        };
        return Task.CompletedTask;
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
