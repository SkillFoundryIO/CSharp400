using LibraryManagement.API.DB;
using LibraryManagement.API.Interfaces;
using LibraryManagement.API.Repositories;
using System.Reflection;
using LibraryManagement.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAll",
                      cors =>
                      {
                            cors.AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader();
                      });
});

builder.Services.AddControllers();
builder.Services.AddDbContext<LibraryContext>();

/* Registering an instance of BorrowerRepository as IBorrowerRepository
   It's registered as transient, which means that a new instance will be created every time the
   IBorrowerRepository constructor parameter is used. */
builder.Services.AddTransient<IBorrowerRepository, BorrowerRepository>();

// if using the extension method instead:
//builder.AddBorrowerRepositories();

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