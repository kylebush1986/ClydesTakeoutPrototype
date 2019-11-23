﻿// <auto-generated />
using System;
using ClydesTakeoutPrototype.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ClydesTakeoutPrototype.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ClydesTakeoutPrototype.Models.MenuModels.Menu", b =>
                {
                    b.Property<decimal>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(20,0)");

                    b.HasKey("ID");

                    b.ToTable("Menu");
                });

            modelBuilder.Entity("ClydesTakeoutPrototype.Models.OrderModels.Item", b =>
                {
                    b.Property<decimal>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(20,0)");

                    b.Property<decimal>("ContainerID")
                        .HasColumnType("decimal(20,0)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("MenuID")
                        .HasColumnType("decimal(20,0)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("OrderID")
                        .HasColumnType("decimal(20,0)");

                    b.Property<TimeSpan>("PrepTime")
                        .HasColumnType("time");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.HasKey("ID");

                    b.HasIndex("MenuID");

                    b.HasIndex("OrderID");

                    b.ToTable("Item");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Item");
                });

            modelBuilder.Entity("ClydesTakeoutPrototype.Models.OrderModels.Order", b =>
                {
                    b.Property<decimal>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(20,0)");

                    b.Property<DateTime>("PickupTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("SpecialInstructions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Total")
                        .HasColumnType("real");

                    b.Property<decimal?>("UserID")
                        .HasColumnType("decimal(20,0)");

                    b.Property<decimal?>("UserID1")
                        .HasColumnType("decimal(20,0)");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.HasIndex("UserID1");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("ClydesTakeoutPrototype.Models.SystemModels.User", b =>
                {
                    b.Property<decimal>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(20,0)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("ClydesTakeoutPrototype.Models.OrderModels.Drink", b =>
                {
                    b.HasBaseType("ClydesTakeoutPrototype.Models.OrderModels.Item");

                    b.Property<int>("DrinkSize")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Drink");
                });

            modelBuilder.Entity("ClydesTakeoutPrototype.Models.OrderModels.Entree", b =>
                {
                    b.HasBaseType("ClydesTakeoutPrototype.Models.OrderModels.Item");

                    b.Property<int>("Category")
                        .HasColumnType("int");

                    b.Property<decimal>("DrinkID")
                        .HasColumnType("decimal(20,0)");

                    b.Property<decimal>("SideID")
                        .HasColumnType("decimal(20,0)");

                    b.HasDiscriminator().HasValue("Entree");
                });

            modelBuilder.Entity("ClydesTakeoutPrototype.Models.OrderModels.Side", b =>
                {
                    b.HasBaseType("ClydesTakeoutPrototype.Models.OrderModels.Item");

                    b.Property<int>("Type")
                        .HasColumnName("Side_Type")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Side");
                });

            modelBuilder.Entity("ClydesTakeoutPrototype.Models.OrderModels.Item", b =>
                {
                    b.HasOne("ClydesTakeoutPrototype.Models.MenuModels.Menu", null)
                        .WithMany("Items")
                        .HasForeignKey("MenuID");

                    b.HasOne("ClydesTakeoutPrototype.Models.OrderModels.Order", null)
                        .WithMany("Items")
                        .HasForeignKey("OrderID");
                });

            modelBuilder.Entity("ClydesTakeoutPrototype.Models.OrderModels.Order", b =>
                {
                    b.HasOne("ClydesTakeoutPrototype.Models.SystemModels.User", null)
                        .WithMany("PendingOrders")
                        .HasForeignKey("UserID");

                    b.HasOne("ClydesTakeoutPrototype.Models.SystemModels.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID1");
                });
#pragma warning restore 612, 618
        }
    }
}
