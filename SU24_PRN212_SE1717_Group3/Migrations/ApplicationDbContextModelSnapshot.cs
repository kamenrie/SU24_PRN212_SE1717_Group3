﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SU24_PRN212_SE1717_Group3.DataAccess;

#nullable disable

namespace SU24_PRN212_SE1717_Group3.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SU24_PRN212_SE1717_Group3.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ProfileId")
                        .HasColumnType("int");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.Property<int?>("ShopId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProfileId");

                    b.HasIndex("RoleId");

                    b.HasIndex("ShopId");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("SU24_PRN212_SE1717_Group3.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("SU24_PRN212_SE1717_Group3.Models.Delivery", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2");

                    b.Property<double?>("Price")
                        .HasColumnType("float");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Delivery");
                });

            modelBuilder.Entity("SU24_PRN212_SE1717_Group3.Models.Discount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Percent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.Property<bool?>("Validity")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Discount");
                });

            modelBuilder.Entity("SU24_PRN212_SE1717_Group3.Models.GeneralFeedback", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AccountId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("FeedbackDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FeedbackText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Ignored")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ResponseDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ResponseText")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("GeneralFeedback");
                });

            modelBuilder.Entity("SU24_PRN212_SE1717_Group3.Models.MyDiscount", b =>
                {
                    b.Property<int?>("DiscountId")
                        .HasColumnType("int");

                    b.Property<int?>("AccountId")
                        .HasColumnType("int");

                    b.HasKey("DiscountId", "AccountId");

                    b.HasIndex("AccountId");

                    b.ToTable("MyDiscount");
                });

            modelBuilder.Entity("SU24_PRN212_SE1717_Group3.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AccountId")
                        .HasColumnType("int");

                    b.Property<int?>("DiscountId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.Property<int?>("ShippingId")
                        .HasColumnType("int");

                    b.Property<int?>("StatusId")
                        .HasColumnType("int");

                    b.Property<double?>("Total")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("DiscountId");

                    b.HasIndex("ShippingId");

                    b.HasIndex("StatusId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("SU24_PRN212_SE1717_Group3.Models.OrderDetail", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("SizeId")
                        .HasColumnType("int");

                    b.Property<int?>("Amount")
                        .HasColumnType("int");

                    b.Property<double?>("Subtotal")
                        .HasColumnType("float");

                    b.HasKey("OrderId", "ProductId", "SizeId");

                    b.HasIndex("ProductId");

                    b.HasIndex("SizeId");

                    b.ToTable("OrderDetail");
                });

            modelBuilder.Entity("SU24_PRN212_SE1717_Group3.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Price")
                        .HasColumnType("float");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.Property<int?>("ShopId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ShopId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("SU24_PRN212_SE1717_Group3.Models.Profile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Image")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Profile");
                });

            modelBuilder.Entity("SU24_PRN212_SE1717_Group3.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("SU24_PRN212_SE1717_Group3.Models.ShippingInformation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeliveryDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DeliveryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RecipientName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DeliveryId");

                    b.ToTable("ShippingInformation");
                });

            modelBuilder.Entity("SU24_PRN212_SE1717_Group3.Models.Shop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Shop");
                });

            modelBuilder.Entity("SU24_PRN212_SE1717_Group3.Models.Size", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Size");
                });

            modelBuilder.Entity("SU24_PRN212_SE1717_Group3.Models.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("SU24_PRN212_SE1717_Group3.Models.Stock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastEditedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Stock");
                });

            modelBuilder.Entity("SU24_PRN212_SE1717_Group3.Models.Account", b =>
                {
                    b.HasOne("SU24_PRN212_SE1717_Group3.Models.Profile", "Profile")
                        .WithMany()
                        .HasForeignKey("ProfileId");

                    b.HasOne("SU24_PRN212_SE1717_Group3.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.HasOne("SU24_PRN212_SE1717_Group3.Models.Shop", "Shop")
                        .WithMany()
                        .HasForeignKey("ShopId");

                    b.Navigation("Profile");

                    b.Navigation("Role");

                    b.Navigation("Shop");
                });

            modelBuilder.Entity("SU24_PRN212_SE1717_Group3.Models.GeneralFeedback", b =>
                {
                    b.HasOne("SU24_PRN212_SE1717_Group3.Models.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId");

                    b.Navigation("Account");
                });

            modelBuilder.Entity("SU24_PRN212_SE1717_Group3.Models.MyDiscount", b =>
                {
                    b.HasOne("SU24_PRN212_SE1717_Group3.Models.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SU24_PRN212_SE1717_Group3.Models.Discount", "Discount")
                        .WithMany()
                        .HasForeignKey("DiscountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Discount");
                });

            modelBuilder.Entity("SU24_PRN212_SE1717_Group3.Models.Order", b =>
                {
                    b.HasOne("SU24_PRN212_SE1717_Group3.Models.Account", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId");

                    b.HasOne("SU24_PRN212_SE1717_Group3.Models.Discount", "Discount")
                        .WithMany()
                        .HasForeignKey("DiscountId");

                    b.HasOne("SU24_PRN212_SE1717_Group3.Models.ShippingInformation", "ShippingInformation")
                        .WithMany()
                        .HasForeignKey("ShippingId");

                    b.HasOne("SU24_PRN212_SE1717_Group3.Models.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId");

                    b.Navigation("Account");

                    b.Navigation("Discount");

                    b.Navigation("ShippingInformation");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("SU24_PRN212_SE1717_Group3.Models.OrderDetail", b =>
                {
                    b.HasOne("SU24_PRN212_SE1717_Group3.Models.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SU24_PRN212_SE1717_Group3.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SU24_PRN212_SE1717_Group3.Models.Size", "Size")
                        .WithMany()
                        .HasForeignKey("SizeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");

                    b.Navigation("Size");
                });

            modelBuilder.Entity("SU24_PRN212_SE1717_Group3.Models.Product", b =>
                {
                    b.HasOne("SU24_PRN212_SE1717_Group3.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.HasOne("SU24_PRN212_SE1717_Group3.Models.Shop", "Shop")
                        .WithMany()
                        .HasForeignKey("ShopId");

                    b.Navigation("Category");

                    b.Navigation("Shop");
                });

            modelBuilder.Entity("SU24_PRN212_SE1717_Group3.Models.ShippingInformation", b =>
                {
                    b.HasOne("SU24_PRN212_SE1717_Group3.Models.Delivery", "Delivery")
                        .WithMany()
                        .HasForeignKey("DeliveryId");

                    b.Navigation("Delivery");
                });
#pragma warning restore 612, 618
        }
    }
}
