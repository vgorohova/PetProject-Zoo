using Microsoft.OpenApi.Models;

using Zoo.API;
using Zoo.Data;
using Zoo.Data.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ZooContext>();
builder.Services.AddScoped<ZooService>();

// builder.Services.AddDbContext<ZooContext>(options =>
//             options.UseNpgsql(Configuration.GetConnectionString("ZooContext")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Zoo API",
        Description = "API for managing a list of animals at the ZOO.",
    });
});

var app = builder.Build();

// TODO: extract those API endpoints to appropriate APIService class 

app.MapGet("/", () => "Hello World! You are at the ZOO.");

app.MapGet("/animals",  async (ZooService s) =>
    await s.GetAllAnimals())
    .WithTags("Get all animals"); 

app.MapGet("/animals/class/{className}", async (string className, ZooService s) =>
    await s.GetAnimalByClass(className))
    .WithTags("Get all animals by their class name");

app.MapGet("/animals/{id}", async (int id, ZooService s) =>
    await s.GetAnimal(id)
        is Animal animal
            ? Results.Ok(animal)
            : Results.NotFound())
    .WithTags("Get animal by Id");

// app.MapPost("/animals", async (Animal animal, ZooService s) =>
// {
//     await s.AddAnimal(animal);
//     return Results.Created($"/animals/{animal.Id}", animal);
// })
//     .WithTags("Add animal to list");

// app.MapPut("/animals/{id}", async (int id, Animal animal, ZooService s) =>
// {
//     await s.UpdateAnimal(id, animal);

//     return Results.NoContent();
// })
//     .WithTags("Update fruit by Id");

// app.MapDelete("/animals/{id}", async (int id, ZooService s) =>
// {
//     if (await s.DeleteAnimal(id))
//     {
//         return Results.Ok(id);
//     }

//     return Results.NotFound();
// })
//     .WithTags("Delete fruit by Id");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.CreateDbIfNotExists();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
