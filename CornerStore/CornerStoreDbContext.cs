using Microsoft.EntityFrameworkCore;
using CornerStore.Models;
public class CornerStoreDbContext : DbContext
{

    public DbSet<Cashier> Cashiers { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }
    public DbSet<Product> Products { get; set; }

    public CornerStoreDbContext(DbContextOptions<CornerStoreDbContext> context) : base(context)
    {

    }

    //allows us to configure the schema when migrating as well as seed data
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cashier>().HasData(new Cashier[]
    {
        new Cashier
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            FullName = "John Doe"
        },
        new Cashier
        {
            Id = 2,
            FirstName = "Jane",
            LastName = "Smith",
            FullName = "Jane Smith"
        },
        new Cashier
        {
            Id = 3,
            FirstName = "Alex",
            LastName = "Taylor",
            FullName = "Alex Taylor"
        }
    });

        modelBuilder.Entity<Category>().HasData(new Category[]
        {
            new Category
            {
                Id = 1,
                CategoryName = "Beverage"
            },
            new Category
            {
                Id = 2,
                CategoryName = "Meat and Deli"
            },
            new Category
            {
                Id = 3,
                CategoryName = "Fresh Produce"
            },
            new Category
            {
                Id = 4,
                CategoryName = "Tobacco Products"
            },
            new Category
            {
                Id = 5,
                CategoryName = "Snacks"
            },
        });

        modelBuilder.Entity<Order>().HasData(new Order[]
        {
            new Order
            {
                Id = 101,
                CashierID = 1,
                Total = 50.75m,
                PaidOnDate = DateTime.Parse("2024-12-10")
            },
            new Order
            {
                Id = 102,
                CashierID = 1,
                Total = 25.00m,
                PaidOnDate = DateTime.Parse("2024-12-11")
            },
            new Order
            {
                Id = 201,
                CashierID = 2,
                Total = 15.25m,
                PaidOnDate = DateTime.Parse("2024-12-10")
            },
            new Order
            {
                Id = 202,
                CashierID = 2,
                Total = 30.00m,
                PaidOnDate = DateTime.Parse("2024-12-12")
            },
            new Order
            {
                Id = 203,
                CashierID = 2,
                Total = 12.50m,
                PaidOnDate = null // This order has not been paid yet
            },
            new Order
            {
                Id = 301,
                CashierID = 3,
                Total = 45.00m,
                PaidOnDate = DateTime.Parse("2024-12-11")
            }
        });

        modelBuilder.Entity<Product>().HasData(new Product[]
        {

            new Product
            {
                Id = 1,
                ProductName = "Coca-Cola",
                Price = 1.50m,
                Brand = "Coca-Cola",
                CategoryId = 1 // Beverage
            },
            new Product
            {
                Id = 2,
                ProductName = "Sprite",
                Price = 1.50m,
                Brand = "Coca-Cola",
                CategoryId = 1 // Beverage
            },
            new Product
            {
                Id = 3,
                ProductName = "Turkey Sandwich",
                Price = 5.99m,
                Brand = "DeliFresh",
                CategoryId = 2 // Meat and Deli
            },
            new Product
            {
                Id = 4,
                ProductName = "Banana",
                Price = 0.25m,
                Brand = "Fresh Farm",
                CategoryId = 3 // Fresh Produce
            },
            new Product
            {
                Id = 5,
                ProductName = "Marlboro Gold Pack",
                Price = 12.00m,
                Brand = "Marlboro",
                CategoryId = 4 // Tobacco Products
            },
            new Product
            {
                Id = 6,
                ProductName = "Lays Classic Chips",
                Price = 1.75m,
                Brand = "Lays",
                CategoryId = 5 // Snacks
            },
            new Product
            {
                Id = 7,
                ProductName = "KitKat Chocolate Bar",
                Price = 1.25m,
                Brand = "KitKat",
                CategoryId = 5 // Snacks
            },
            new Product
            {
                Id = 8,
                ProductName = "Orange Juice",
                Price = 2.50m,
                Brand = "Tropicana",
                CategoryId = 1 // Beverage
            },
            new Product
            {
                Id = 9,
                ProductName = "Ham Sandwich",
                Price = 5.99m,
                Brand = "DeliFresh",
                CategoryId = 2 // Meat and Deli
            },
            new Product
            {
                Id = 10,
                ProductName = "Apple",
                Price = 0.50m,
                Brand = "Fresh Farm",
                CategoryId = 3 // Fresh Produce
            },
                    new Product
            {
                Id = 11,
                ProductName = "Pepsi",
                Price = 1.50m,
                Brand = "Pepsi",
                CategoryId = 1 // Beverage
            },
            new Product
            {
                Id = 12,
                ProductName = "Aquafina Water",
                Price = 1.00m,
                Brand = "Aquafina",
                CategoryId = 1 // Beverage
            },
            new Product
            {
                Id = 13,
                ProductName = "Beef Jerky",
                Price = 3.99m,
                Brand = "Jack Link's",
                CategoryId = 5 // Snacks
            },
            new Product
            {
                Id = 14,
                ProductName = "Egg Sandwich",
                Price = 4.99m,
                Brand = "DeliFresh",
                CategoryId = 2 // Meat and Deli
            },
            new Product
            {
                Id = 15,
                ProductName = "Carrot",
                Price = 0.30m,
                Brand = "Fresh Farm",
                CategoryId = 3 // Fresh Produce
            },
            new Product
            {
                Id = 16,
                ProductName = "Camel Blue Pack",
                Price = 11.50m,
                Brand = "Camel",
                CategoryId = 4 // Tobacco Products
            },
            new Product
            {
                Id = 17,
                ProductName = "Snickers Bar",
                Price = 1.25m,
                Brand = "Snickers",
                CategoryId = 5 // Snacks
            },
            new Product
            {
                Id = 18,
                ProductName = "Energy Drink",
                Price = 2.99m,
                Brand = "Red Bull",
                CategoryId = 1 // Beverage
            },
            new Product
            {
                Id = 19,
                ProductName = "Hot Dog",
                Price = 2.50m,
                Brand = "Oscar Mayer",
                CategoryId = 2 // Meat and Deli
            },
            new Product
            {
                Id = 20,
                ProductName = "Potato",
                Price = 0.20m,
                Brand = "Fresh Farm",
                CategoryId = 3 // Fresh Produce
            }
        });

        modelBuilder.Entity<OrderProduct>()
    .HasKey(op => new { op.OrderId, op.ProductId }); // Define composite primary key for OrderProduct

        // Configure relationships for OrderProduct
        modelBuilder.Entity<OrderProduct>()
            .HasOne<Order>(op => op.Order)
            .WithMany(o => o.OrderProducts)
            .HasForeignKey(op => op.OrderId);

        modelBuilder.Entity<OrderProduct>()
            .HasOne<Product>(op => op.Product)
            .WithMany(p => p.OrderProducts)
            .HasForeignKey(op => op.ProductId);


        modelBuilder.Entity<OrderProduct>().HasData(new OrderProduct[]
        {
            // Order 101 by Cashier 1
            new OrderProduct
            {
                ProductId = 1, // Coca-Cola
                OrderId = 101,
                Quantity = 2
            },
            new OrderProduct
            {
                ProductId = 3, // Turkey Sandwich
                OrderId = 101,
                Quantity = 1
            },

            // Order 102 by Cashier 1
            new OrderProduct
            {
                ProductId = 6, // Lays Classic Chips
                OrderId = 102,
                Quantity = 3
            },
            new OrderProduct
            {
                ProductId = 7, // KitKat Chocolate Bar
                OrderId = 102,
                Quantity = 2
            },

            // Order 201 by Cashier 2
            new OrderProduct
            {
                ProductId = 4, // Banana
                OrderId = 201,
                Quantity = 6
            },
            new OrderProduct
            {
                ProductId = 9, // Ham Sandwich
                OrderId = 201,
                Quantity = 1
            },

            // Order 202 by Cashier 2
            new OrderProduct
            {
                ProductId = 2, // Sprite
                OrderId = 202,
                Quantity = 2
            },
            new OrderProduct
            {
                ProductId = 5, // Marlboro Gold Pack
                OrderId = 202,
                Quantity = 1
            },

            // Order 203 by Cashier 2
            new OrderProduct
            {
                ProductId = 17, // Snickers Bar
                OrderId = 203,
                Quantity = 4
            },
            new OrderProduct
            {
                ProductId = 13, // Beef Jerky
                OrderId = 203,
                Quantity = 1
            },

            // Order 301 by Cashier 3
            new OrderProduct
            {
                ProductId = 18, // Energy Drink
                OrderId = 301,
                Quantity = 2
            },
            new OrderProduct
            {
                ProductId = 19, // Hot Dog
                OrderId = 301,
                Quantity = 1
            }
        });

    }
}