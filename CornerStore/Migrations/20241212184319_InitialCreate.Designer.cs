﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CornerStore.Migrations
{
    [DbContext(typeof(CornerStoreDbContext))]
    [Migration("20241212184319_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CornerStore.Models.Cashier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Cashiers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FirstName = "John",
                            FullName = "John Doe",
                            LastName = "Doe"
                        },
                        new
                        {
                            Id = 2,
                            FirstName = "Jane",
                            FullName = "Jane Smith",
                            LastName = "Smith"
                        },
                        new
                        {
                            Id = 3,
                            FirstName = "Alex",
                            FullName = "Alex Taylor",
                            LastName = "Taylor"
                        });
                });

            modelBuilder.Entity("CornerStore.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryName = "Beverage"
                        },
                        new
                        {
                            Id = 2,
                            CategoryName = "Meat and Deli"
                        },
                        new
                        {
                            Id = 3,
                            CategoryName = "Fresh Produce"
                        },
                        new
                        {
                            Id = 4,
                            CategoryName = "Tobacco Products"
                        },
                        new
                        {
                            Id = 5,
                            CategoryName = "Snacks"
                        });
                });

            modelBuilder.Entity("CornerStore.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CashierID")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("PaidOnDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<decimal>("Total")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("CashierID");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            Id = 101,
                            CashierID = 1,
                            PaidOnDate = new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Total = 50.75m
                        },
                        new
                        {
                            Id = 102,
                            CashierID = 1,
                            PaidOnDate = new DateTime(2024, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Total = 25.00m
                        },
                        new
                        {
                            Id = 201,
                            CashierID = 2,
                            PaidOnDate = new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Total = 15.25m
                        },
                        new
                        {
                            Id = 202,
                            CashierID = 2,
                            PaidOnDate = new DateTime(2024, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Total = 30.00m
                        },
                        new
                        {
                            Id = 203,
                            CashierID = 2,
                            Total = 12.50m
                        },
                        new
                        {
                            Id = 301,
                            CashierID = 3,
                            PaidOnDate = new DateTime(2024, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Total = 45.00m
                        });
                });

            modelBuilder.Entity("CornerStore.Models.OrderProduct", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("integer");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("OrderId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderProducts");

                    b.HasData(
                        new
                        {
                            OrderId = 101,
                            ProductId = 1,
                            Quantity = 2
                        },
                        new
                        {
                            OrderId = 101,
                            ProductId = 3,
                            Quantity = 1
                        },
                        new
                        {
                            OrderId = 102,
                            ProductId = 6,
                            Quantity = 3
                        },
                        new
                        {
                            OrderId = 102,
                            ProductId = 7,
                            Quantity = 2
                        },
                        new
                        {
                            OrderId = 201,
                            ProductId = 4,
                            Quantity = 6
                        },
                        new
                        {
                            OrderId = 201,
                            ProductId = 9,
                            Quantity = 1
                        },
                        new
                        {
                            OrderId = 202,
                            ProductId = 2,
                            Quantity = 2
                        },
                        new
                        {
                            OrderId = 202,
                            ProductId = 5,
                            Quantity = 1
                        },
                        new
                        {
                            OrderId = 203,
                            ProductId = 17,
                            Quantity = 4
                        },
                        new
                        {
                            OrderId = 203,
                            ProductId = 13,
                            Quantity = 1
                        },
                        new
                        {
                            OrderId = 301,
                            ProductId = 18,
                            Quantity = 2
                        },
                        new
                        {
                            OrderId = 301,
                            ProductId = 19,
                            Quantity = 1
                        });
                });

            modelBuilder.Entity("CornerStore.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Brand = "Coca-Cola",
                            CategoryId = 1,
                            Price = 1.50m,
                            ProductName = "Coca-Cola"
                        },
                        new
                        {
                            Id = 2,
                            Brand = "Coca-Cola",
                            CategoryId = 1,
                            Price = 1.50m,
                            ProductName = "Sprite"
                        },
                        new
                        {
                            Id = 3,
                            Brand = "DeliFresh",
                            CategoryId = 2,
                            Price = 5.99m,
                            ProductName = "Turkey Sandwich"
                        },
                        new
                        {
                            Id = 4,
                            Brand = "Fresh Farm",
                            CategoryId = 3,
                            Price = 0.25m,
                            ProductName = "Banana"
                        },
                        new
                        {
                            Id = 5,
                            Brand = "Marlboro",
                            CategoryId = 4,
                            Price = 12.00m,
                            ProductName = "Marlboro Gold Pack"
                        },
                        new
                        {
                            Id = 6,
                            Brand = "Lays",
                            CategoryId = 5,
                            Price = 1.75m,
                            ProductName = "Lays Classic Chips"
                        },
                        new
                        {
                            Id = 7,
                            Brand = "KitKat",
                            CategoryId = 5,
                            Price = 1.25m,
                            ProductName = "KitKat Chocolate Bar"
                        },
                        new
                        {
                            Id = 8,
                            Brand = "Tropicana",
                            CategoryId = 1,
                            Price = 2.50m,
                            ProductName = "Orange Juice"
                        },
                        new
                        {
                            Id = 9,
                            Brand = "DeliFresh",
                            CategoryId = 2,
                            Price = 5.99m,
                            ProductName = "Ham Sandwich"
                        },
                        new
                        {
                            Id = 10,
                            Brand = "Fresh Farm",
                            CategoryId = 3,
                            Price = 0.50m,
                            ProductName = "Apple"
                        },
                        new
                        {
                            Id = 11,
                            Brand = "Pepsi",
                            CategoryId = 1,
                            Price = 1.50m,
                            ProductName = "Pepsi"
                        },
                        new
                        {
                            Id = 12,
                            Brand = "Aquafina",
                            CategoryId = 1,
                            Price = 1.00m,
                            ProductName = "Aquafina Water"
                        },
                        new
                        {
                            Id = 13,
                            Brand = "Jack Link's",
                            CategoryId = 5,
                            Price = 3.99m,
                            ProductName = "Beef Jerky"
                        },
                        new
                        {
                            Id = 14,
                            Brand = "DeliFresh",
                            CategoryId = 2,
                            Price = 4.99m,
                            ProductName = "Egg Sandwich"
                        },
                        new
                        {
                            Id = 15,
                            Brand = "Fresh Farm",
                            CategoryId = 3,
                            Price = 0.30m,
                            ProductName = "Carrot"
                        },
                        new
                        {
                            Id = 16,
                            Brand = "Camel",
                            CategoryId = 4,
                            Price = 11.50m,
                            ProductName = "Camel Blue Pack"
                        },
                        new
                        {
                            Id = 17,
                            Brand = "Snickers",
                            CategoryId = 5,
                            Price = 1.25m,
                            ProductName = "Snickers Bar"
                        },
                        new
                        {
                            Id = 18,
                            Brand = "Red Bull",
                            CategoryId = 1,
                            Price = 2.99m,
                            ProductName = "Energy Drink"
                        },
                        new
                        {
                            Id = 19,
                            Brand = "Oscar Mayer",
                            CategoryId = 2,
                            Price = 2.50m,
                            ProductName = "Hot Dog"
                        },
                        new
                        {
                            Id = 20,
                            Brand = "Fresh Farm",
                            CategoryId = 3,
                            Price = 0.20m,
                            ProductName = "Potato"
                        });
                });

            modelBuilder.Entity("CornerStore.Models.Order", b =>
                {
                    b.HasOne("CornerStore.Models.Cashier", null)
                        .WithMany("Orders")
                        .HasForeignKey("CashierID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CornerStore.Models.OrderProduct", b =>
                {
                    b.HasOne("CornerStore.Models.Order", "Order")
                        .WithMany("OrderProducts")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CornerStore.Models.Product", "Product")
                        .WithMany("OrderProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("CornerStore.Models.Product", b =>
                {
                    b.HasOne("CornerStore.Models.Category", null)
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CornerStore.Models.Cashier", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("CornerStore.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("CornerStore.Models.Order", b =>
                {
                    b.Navigation("OrderProducts");
                });

            modelBuilder.Entity("CornerStore.Models.Product", b =>
                {
                    b.Navigation("OrderProducts");
                });
#pragma warning restore 612, 618
        }
    }
}
