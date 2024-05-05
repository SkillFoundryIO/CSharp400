using LibraryManagement.API.DB;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<LibraryContext>();
var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();