using MinimalApiDemo.Data;
using MinimalApiDemo.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("GameConnection");
builder.Services.AddSqlite<GameStoreContext>(connectionString);

var app = builder.Build();

//var products = new List<Product>
//{
//    new(1, "Laptop", 999.99),
//    new(2, "Phone", 499.99)
//};

//app.MapGet("/products", () => products);

//app.MapGet("/products/{id}", (int id) =>
//    products.FirstOrDefault(p => p.Id == id) is Product product
//        ? Results.Ok(product)
//        : Results.NotFound());

//app.MapPost("/products", (Product product) =>
//{
//    products.Add(product);
//    return Results.Created($"/products/{product.Id}", product);
//});


app.MapGroup("/games").MapGamesEndpoints();

app.Run();
