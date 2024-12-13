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
                    ProductName = op.Product.ProductName,
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

app.MapGet("/api/products", async (CornerStoreDbContext db, string? search) =>
{
    var productsQuery = db.Products.Include(p => p.Category).AsQueryable();

    if (!string.IsNullOrEmpty(search))
    {
        productsQuery = productsQuery.Where(p =>
            p.ProductName.ToLower().Contains(search.ToLower()) ||
            p.Category.CategoryName.ToLower().Contains(search.ToLower()));
    }

    var products = await productsQuery.Select(p => new ProductDTO
    {
        Id = p.Id,
        ProductName = p.ProductName,
        Price = p.Price,
        Category = new ProductCategoryDTO
        {
            Id = p.Category.Id,
            CategoryName = p.Category.CategoryName
        }
    }).ToListAsync();

    return Results.Ok(products);
});


app.MapPost("/api/products", async (CornerStoreDbContext db, NewProductDTO newProductDto) =>
{
    var product = new Product
    {
        ProductName = newProductDto.ProductName,
        Price = newProductDto.Price,
        Brand = newProductDto.Brand,
        CategoryId = newProductDto.CategoryId
    };

    db.Products.Add(product);
    await db.SaveChangesAsync();

    var createdProduct = new ProductDTO
    {
        Id = product.Id,
        ProductName = product.ProductName,
        Price = product.Price,
        Category = new ProductCategoryDTO
        {
            Id = product.CategoryId,
            CategoryName = (await db.Categories.FindAsync(product.CategoryId))?.CategoryName
        }
    };

    return Results.Created($"/api/products/{product.Id}", createdProduct);
});


app.MapPut("/api/products/{id}", async (CornerStoreDbContext db, int id, UpdateProductDTO updateProductDto) =>
{
    var product = await db.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);

    if (product == null)
    {
        return Results.NotFound();
    }

    if (!string.IsNullOrEmpty(updateProductDto.ProductName))
    {
        product.ProductName = updateProductDto.ProductName;
    }

    if (updateProductDto.Price.HasValue)
    {
        product.Price = updateProductDto.Price.Value;
    }

    if (updateProductDto.CategoryId.HasValue)
    {
        var category = await db.Categories.FindAsync(updateProductDto.CategoryId.Value);
        if (category == null)
        {
            return Results.BadRequest("Invalid CategoryId.");
        }
        product.CategoryId = updateProductDto.CategoryId.Value;
    }

    await db.SaveChangesAsync();

    var updatedProduct = new ProductDTO
    {
        Id = product.Id,
        ProductName = product.ProductName,
        Price = product.Price,
        Category = product.Category != null
            ? new ProductCategoryDTO
            {
                Id = product.Category.Id,
                CategoryName = product.Category.CategoryName
            }
            : null
    };

    return Results.Ok(updatedProduct);

});






/*------ORDERS-------*/

app.MapGet("/api/orders/{id}", async (CornerStoreDbContext db, int id) =>
{
    var order = await db.Orders
        .Include(o => o.Cashier)
        .Include(o => o.OrderProducts)
        .ThenInclude(op => op.Product)
        .ThenInclude(p => p.Category)
        .Where(o => o.Id == id)
        .Select(o => new OrderByIdDTO
        {
            Id = o.Id,
            Cashier = o.Cashier.FullName,
            Total = o.Total,
            PaidOnDate = o.PaidOnDate,
            OrderProducts = o.OrderProducts.Select(op => new OrderProductWithCategoryDTO
            {
                ProductId = op.ProductId,
                ProductName = op.Product.ProductName,
                Quantity = op.Quantity,
                Category = new ProductCategoryDTO
                {
                    Id = op.Product.Category.Id,
                    CategoryName = op.Product.Category.CategoryName
                }
            }).ToList()
        })
        .FirstOrDefaultAsync();

    if (order == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(order);
});



app.MapGet("/api/orders", async (CornerStoreDbContext db, DateTime? orderDate) =>
{
    var query = db.Orders.Include(o => o.Cashier).Include(o => o.OrderProducts).ThenInclude(op => op.Product).ThenInclude(p => p.Category).AsQueryable();

    if (orderDate.HasValue)
    {
        query = query.Where(o => o.PaidOnDate != null && o.PaidOnDate.Value.Date == orderDate.Value.Date);
    }

    var orders = await query.Select(o => new OrderDTO
    {
        Id = o.Id,
        Total = o.Total,
        PaidOnDate = o.PaidOnDate,
        OrderProducts = o.OrderProducts.Select(op => new OrderProductDTO
        {
            ProductId = op.ProductId,
            ProductName = op.Product.ProductName,
            Quantity = op.Quantity
        }).ToList()
    }).ToListAsync();

    return Results.Ok(orders);
});

app.MapDelete("/api/orders/{id}", async (CornerStoreDbContext db, int id) =>
{
    var order = await db.Orders.FindAsync(id);

    if (order == null)
    {
        return Results.NotFound();
    }

    db.Orders.Remove(order);
    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapPost("/api/orders", async (CornerStoreDbContext db, NewOrderDTO newOrderDto) =>
{
    var cashier = await db.Cashiers.FindAsync(newOrderDto.CashierID);
    if (cashier == null)
    {
        return Results.BadRequest("Invalid Cashier ID.");
    }

    var order = new Order
    {
        CashierID = newOrderDto.CashierID,
        Total = newOrderDto.OrderProducts.Sum(op => op.Quantity * (db.Products.Find(op.ProductId)?.Price ?? 0)),
        PaidOnDate = newOrderDto.PaidOnDate
    };

    db.Orders.Add(order);
    await db.SaveChangesAsync();

    foreach (var orderProductDto in newOrderDto.OrderProducts)
    {
        var product = await db.Products.FindAsync(orderProductDto.ProductId);
        if (product == null)
        {
            return Results.BadRequest($"Invalid Product ID: {orderProductDto.ProductId}");
        }

        db.OrderProducts.Add(new OrderProduct
        {
            OrderId = order.Id,
            ProductId = orderProductDto.ProductId,
            Quantity = orderProductDto.Quantity
        });
    }

    await db.SaveChangesAsync();

    return Results.Created($"/api/orders/{order.Id}", new { order.Id });
});





app.Run();

//don't move or change this!
public partial class Program { }