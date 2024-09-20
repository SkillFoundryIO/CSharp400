using LibraryManagement.API;
using LibraryManagement.Application;
using LibraryManagement.Core.Interfaces.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

// configure dependency injection for the service factory
var config = new AppConfiguration();
var serviceFactory = new ServiceFactory(config);

builder.Services.AddScoped<IBorrowerService>(_ => serviceFactory.CreateBorrowerService());
builder.Services.AddScoped<ICheckoutService>(_ => serviceFactory.CreateCheckoutService());
builder.Services.AddScoped<IMediaService>(_ => serviceFactory.CreateMediaService());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
