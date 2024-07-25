using LibraryManagement.Application;
using LibraryManagement.MVC;
using LibraryManagement.MVC.Utilities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

var config = new AppConfiguration();
ServiceFactory sf = new(config);

builder.Services.AddScoped(provider => sf.CreateMediaService());
builder.Services.AddScoped(provider => sf.CreateBorrowerService());
builder.Services.AddScoped(provider => sf.CreateCheckoutService());
builder.Services.AddScoped<ISelectListBuilder, SelectListBuilder>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
