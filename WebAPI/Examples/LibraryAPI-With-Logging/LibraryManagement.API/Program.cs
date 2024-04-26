using LibraryManagement.API.DB;
using LibraryManagement.API.Interfaces;
using LibraryManagement.API.Repositories;
using Serilog;
using Serilog.Events;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// The inbuilt logging configuration
// builder.Logging.ClearProviders();
// builder.Logging.AddConsole();

// Serilog logging configuration
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .WriteTo.Console()
    .WriteTo.File("log.txt",
        rollingInterval: RollingInterval.Day,
        rollOnFileSizeLimit: true)
    .CreateLogger();

builder.Services.AddLogging(loggingBuilder =>
        loggingBuilder.AddSerilog(dispose: true));

builder.Services.AddControllers();
builder.Services.AddDbContext<LibraryContext>();

builder.Services.AddTransient<IBorrowerRepository, BorrowerRepository>();

builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();