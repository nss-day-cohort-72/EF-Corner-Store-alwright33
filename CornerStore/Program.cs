using CornerStore.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using CornerStore.Migrations;
using CornerStore.Models.DTOs;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// allows passing datetimes without time zone data 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// allows our api endpoints to access the database through Entity Framework Core and provides dummy value for testing
builder.Services.AddNpgsql<CornerStoreDbContext>(builder.Configuration["CornerStoreDbConnectionString"] ?? "testing");

// Set the JSON serializer options
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

/*------CASHIERS-------*/

app.MapGet("/api/cashiers/{id}", async (CornerStoreDbContext db, int id) =>
{
    var cashier = await db.Cashiers
        .Include(c => c.Orders) // Include Orders
        .ThenInclude(o => o.OrderProducts) // Include OrderProducts
        .Where(c => c.Id == id) // Filter by Cashier ID
        .Select(c => new CashierDTO
        {
            Id = c.Id,
            FullName = c.FullName,
            Orders = c.Orders.Select(o => new OrderDTO
            {
                Id = o.Id,
                Total = o.Total,
                OrderProducts = o.OrderProducts.Select(op => new OrderProductDTO
                {
                    ProductId = op.ProductId,
                    OrderId = op.OrderId,
                    Quantity = op.Quantity
                }).ToList()
            }).ToList()
        })
        .FirstOrDefaultAsync();

    if (cashier == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(cashier);
});





app.MapPost("/api/cashiers", async (CornerStoreDbContext db, NewCashierDTO newCashierDto) =>
{
    var cashier = new Cashier
    {
        FirstName = newCashierDto.FirstName,
        LastName = newCashierDto.LastName,
        FullName = newCashierDto.FirstName + " " + newCashierDto.LastName
    };

    db.Cashiers.Add(cashier);
    await db.SaveChangesAsync();

    var createdCashier = await db.Cashiers
        .Where(c => c.Id == cashier.Id)
        .Select(c => new CashierDTO
        {
            Id = c.Id,
            FullName = c.FirstName + " " + c.LastName
        })
        .FirstOrDefaultAsync();

    return Results.Created($"/api/cashiers/{cashier.Id}", createdCashier);
});


// /*------PRODUCTS-------*/

// app.MapGet("/api/products", (CornerStoreDbContext db) =>
// {

// });

// app.MapPost("api/products", (CornerStoreDbContext db) =>
// {

// });

// app.MapPut("/api/products/{id}", (CornerStoreDbContext db) =>
// {

// });

// /*------ORDERS-------*/

// app.MapGet("/api/orders", (CornerStoreDbContext db) =>
// {

// });
// app.MapGet("/api/orders/{id}", (CornerStoreDbContext db) =>
// {

// });

// app.MapPost("/api/products", (CornerStoreDbContext db) =>
// {

// });

// app.MapDelete("/api/products/{id}", (CornerStoreDbContext db) =>
// {

// });




app.Run();

//don't move or change this!
public partial class Program { }