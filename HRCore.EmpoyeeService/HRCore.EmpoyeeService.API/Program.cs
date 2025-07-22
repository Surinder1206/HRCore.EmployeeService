using Asp.Versioning;
using HRCore.EmpoyeeService.API.Documentation;
using HRCore.EmpoyeeService.API.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddEmployeeDbContext(builder.Configuration);
builder.Services.AddEmployeeServices();

// Authentication and Authorisation

/* //Basic authentication setup, uncomment if needed option: 1
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(jwtOptions =>
{
jwtOptions.Authority = "https://sts.windows.net/3b27d02e-0ad3-4e02-947d-dd47cf86624f/"; // authority
jwtOptions.Audience = "3f91483c-7acd-4269-a360-c62dafb0cf7d"; //clientId
});
*/

//option: 2
//var configurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(
//          $"https://sts.windows.net/3b27d02e-0ad3-4e02-947d-dd47cf86624f/.well-known/openid-configuration",
//          new OpenIdConnectConfigurationRetriever(),
//          new HttpDocumentRetriever());

//var discoveryDocument = await configurationManager.GetConfigurationAsync();
//var issuerSigningKeys = discoveryDocument.SigningKeys;

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(jwtOptions =>
//    {
//        jwtOptions.Authority = builder.Configuration["AzureAd:Authority"];
//        jwtOptions.Audience = builder.Configuration["AzureAd:Audience"];
//        jwtOptions.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,
//            IssuerSigningKeys = issuerSigningKeys,
//            ValidAudiences = builder.Configuration.GetSection("AzureAd:ValidAudiences").Get<string[]>(),
//            ValidIssuers = builder.Configuration.GetSection("AzureAd:ValidIssuers").Get<string[]>(),
//            RoleClaimType = "roles",
//            NameClaimType = "name"
//        };
//        jwtOptions.MapInboundClaims = false;
//    });

//option: 3
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
    AddJwtBearer(options => builder.Configuration.Bind("AzureAd", options));

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
