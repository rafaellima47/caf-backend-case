using FaceComparisonAPI.Services;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using FaceComparisonAPI.Database;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

// Add services to the container.
builder.Host.UseSerilog();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FaceComparisonAPI", Version = "v1" });
});

// Add your custom services here
builder.Services.AddScoped<IAnonymizationService>(sp =>
{
    var httpClient = sp.GetRequiredService<HttpClient>();
    var anonymizeApiUrl = "http://anonymization-service:5000/anonymize";
    return new AnonymizationService(httpClient, anonymizeApiUrl);
});

builder.Services.AddScoped<IDatabaseService, DatabaseService>();
builder.Services.AddScoped<IComparisonService, ComparisonService>();

// Register HttpClient
builder.Services.AddHttpClient();

// Comparison threshold configuration
builder.Services.AddSingleton<ComparisonConfiguration>(sp => new ComparisonConfiguration
{
    Threshold = Convert.ToDouble(builder.Configuration["ComparisonThreshold"])
});


// Configure Entity Framework
builder.Services.AddDbContext<FaceComparisonDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("FaceComparisonDb"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FaceComparisonAPI v1"));
}

app.UseAuthorization();

app.MapControllers();

app.Run();
