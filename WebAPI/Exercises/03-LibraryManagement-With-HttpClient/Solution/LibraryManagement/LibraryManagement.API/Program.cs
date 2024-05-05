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

var config = new AppConfiguration();
var serviceFactory = new ServiceFactory(config);
var borrowerService = serviceFactory.CreateBorrowerService();
var checkoutService = serviceFactory.CreateCheckoutService();
var mediaService = serviceFactory.CreateMediaService();

builder.Services.AddSingleton<IBorrowerService>(borrowerService);
builder.Services.AddSingleton<ICheckoutService>(checkoutService);
builder.Services.AddSingleton<IMediaService>(mediaService);

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
