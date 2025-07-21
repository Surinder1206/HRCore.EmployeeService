using Asp.Versioning;
using HRCore.EmpoyeeService.API.Documentation;
using HRCore.EmpoyeeService.API.Infrastructure;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddEmployeeDbContext(builder.Configuration);
builder.Services.AddEmployeeServices();

builder.Services.AddAuthentication().AddJwtBearer();

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
    options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
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
    options.AddOperationTransformer((operation, context, cancellationToken) =>
    {
        operation.Responses.Add("500", new OpenApiResponse { Description = "Internal server error" });
        return Task.CompletedTask;
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();


    //app.UseSwaggerUI(options =>
    //{
    //    options.DocumentTitle = "Employee Service API Documentation";
    //    options.SwaggerEndpoint("/openapi/v1.json", "Employee Service API v1");
    //});

    //app.UseReDoc(options =>
    //{
    //    options.DocumentTitle = "Employee Service API Documentation";
    //    options.SpecUrl = "/openapi/v1.json";
    //});

    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
