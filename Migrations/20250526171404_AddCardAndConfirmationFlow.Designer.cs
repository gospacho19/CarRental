﻿// <auto-generated />
using System;
using LuxuryCarRental.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LuxuryCarRental.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250526171404_AddCardAndConfirmationFlow")]
    partial class AddCardAndConfirmationFlow
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.5");

            modelBuilder.Entity("LuxuryCarRental.Models.Basket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CustomerId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Baskets");
                });

            modelBuilder.Entity("LuxuryCarRental.Models.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("DailyRate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("TEXT");

                    b.Property<string>("Make")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Year")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("Make", "Model");

                    b.ToTable("Cars");

                    b.HasDiscriminator().HasValue("Car");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("LuxuryCarRental.Models.Card", b =>
                {
                    b.Property<int>("CardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CardNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("CustomerId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Cvv")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ExpiryMonth")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ExpiryYear")
                        .HasColumnType("INTEGER");

                    b.HasKey("CardId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Cards");
                });

            modelBuilder.Entity("LuxuryCarRental.Models.CartItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BasketId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CarId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Subtotal")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BasketId");

                    b.HasIndex("CarId");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("LuxuryCarRental.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DriverLicenseNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsBlacklisted")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("LuxuryCarRental.Models.Rental", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CarId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CustomerId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("TotalCost")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Rentals");
                });

            modelBuilder.Entity("LuxuryCarRental.Models.LuxuryCar", b =>
                {
                    b.HasBaseType("LuxuryCarRental.Models.Car");

                    b.Property<bool>("IncludesChauffeur")
                        .HasColumnType("INTEGER");

                    b.Property<string>("OptionalFeatures")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("SecurityDeposit")
                        .HasColumnType("TEXT");

                    b.HasDiscriminator().HasValue("LuxuryCar");
                });

            modelBuilder.Entity("LuxuryCarRental.Models.Basket", b =>
                {
                    b.HasOne("LuxuryCarRental.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("LuxuryCarRental.Models.Card", b =>
                {
                    b.HasOne("LuxuryCarRental.Models.Customer", "Customer")
                        .WithMany("Cards")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("LuxuryCarRental.Models.CartItem", b =>
                {
                    b.HasOne("LuxuryCarRental.Models.Basket", "Basket")
                        .WithMany("Items")
                        .HasForeignKey("BasketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LuxuryCarRental.Models.Car", "Car")
                        .WithMany()
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Basket");

                    b.Navigation("Car");
                });

            modelBuilder.Entity("LuxuryCarRental.Models.Customer", b =>
                {
                    b.OwnsOne("LuxuryCarRental.Models.ContactInfo", "Contact", b1 =>
                        {
                            b1.Property<int>("CustomerId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("Email")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.Property<string>("Phone")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.HasKey("CustomerId");

                            b1.ToTable("Customers");

                            b1.WithOwner()
                                .HasForeignKey("CustomerId");
                        });

                    b.Navigation("Contact")
                        .IsRequired();
                });

            modelBuilder.Entity("LuxuryCarRental.Models.Rental", b =>
                {
                    b.HasOne("LuxuryCarRental.Models.Car", "Car")
                        .WithMany()
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LuxuryCarRental.Models.Customer", "Customer")
                        .WithMany("Rentals")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("LuxuryCarRental.Models.Basket", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("LuxuryCarRental.Models.Customer", b =>
                {
                    b.Navigation("Cards");

                    b.Navigation("Rentals");
                });
#pragma warning restore 612, 618
        }
    }
}
