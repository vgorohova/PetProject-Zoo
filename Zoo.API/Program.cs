using Zoo.Data;
using Zoo.API;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ZooContext>();
builder.Services.AddScoped<ZooService>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.CreateDbIfNotExists();

app.Run();
