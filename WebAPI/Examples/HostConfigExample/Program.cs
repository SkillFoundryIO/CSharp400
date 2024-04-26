using HelloAspNet;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Remove the following to use launchSettings.json
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(3000);

    options.ListenAnyIP(5000, listenOptions =>
    {
        listenOptions.UseHttps();
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: Constants.AllowAnyGetOrigins,
        policy =>
        {
            policy.WithOrigins("*");
            policy.WithMethods("GET");
        });
});

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

app.UseCors();

app.Run();
