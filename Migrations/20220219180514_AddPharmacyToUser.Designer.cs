﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyPharmacy.Entities;

namespace MyPharmacy.Migrations
{
    [DbContext(typeof(PharmacyDbContext))]
    [Migration("20220219180514_AddPharmacyToUser")]
    partial class AddPharmacyToUser
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("DrugCategoryDrugInformation", b =>
                {
                    b.Property<int>("DrugCategoriesId")
                        .HasColumnType("int");

                    b.Property<int>("DrugInformationsId")
                        .HasColumnType("int");

                    b.HasKey("DrugCategoriesId", "DrugInformationsId");

                    b.HasIndex("DrugInformationsId");

                    b.ToTable("DrugCategoryDrugInformation");
                });

            modelBuilder.Entity("DrugOrderByClient", b =>
                {
                    b.Property<int>("DrugsId")
                        .HasColumnType("int");

                    b.Property<int>("OrderByClientsId")
                        .HasColumnType("int");

                    b.HasKey("DrugsId", "OrderByClientsId");

                    b.HasIndex("OrderByClientsId");

                    b.ToTable("DrugOrderByClient");
                });

            modelBuilder.Entity("MyPharmacy.Entities.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("MyPharmacy.Entities.Drug", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("AmountOfPackages")
                        .HasColumnType("int");

                    b.Property<int?>("DrugInformationId")
                        .HasColumnType("int");

                    b.Property<int>("PharmacyId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("DrugInformationId");

                    b.HasIndex("PharmacyId");

                    b.ToTable("Drugs");
                });

            modelBuilder.Entity("MyPharmacy.Entities.DrugCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DrugCategories");
                });

            modelBuilder.Entity("MyPharmacy.Entities.DrugInformation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DrugsName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LumpSumDrug")
                        .HasColumnType("bit");

                    b.Property<int>("MilligramsPerTablets")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfTablets")
                        .HasColumnType("int");

                    b.Property<bool>("PrescriptionRequired")
                        .HasColumnType("bit");

                    b.Property<string>("SubstancesName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("DrugInformations");
                });

            modelBuilder.Entity("MyPharmacy.Entities.OrderByClient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("AddressId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateOfOrder")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOfReceipt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsPersonalPickup")
                        .HasColumnType("bit");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("StatusId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AddressId")
                        .IsUnique()
                        .HasFilter("[AddressId] IS NOT NULL");

                    b.HasIndex("StatusId");

                    b.HasIndex("UserId");

                    b.ToTable("OrderByClients");
                });

            modelBuilder.Entity("MyPharmacy.Entities.Pharmacy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<string>("ContactEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CreatedByUserId")
                        .HasColumnType("int");

                    b.Property<bool>("HasPresciptionDrugs")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId")
                        .IsUnique();

                    b.ToTable("Pharmacies");
                });

            modelBuilder.Entity("MyPharmacy.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("MyPharmacy.Entities.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Statuses");
                });

            modelBuilder.Entity("MyPharmacy.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nationality")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PharmacyId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PharmacyId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DrugCategoryDrugInformation", b =>
                {
                    b.HasOne("MyPharmacy.Entities.DrugCategory", null)
                        .WithMany()
                        .HasForeignKey("DrugCategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyPharmacy.Entities.DrugInformation", null)
                        .WithMany()
                        .HasForeignKey("DrugInformationsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DrugOrderByClient", b =>
                {
                    b.HasOne("MyPharmacy.Entities.Drug", null)
                        .WithMany()
                        .HasForeignKey("DrugsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyPharmacy.Entities.OrderByClient", null)
                        .WithMany()
                        .HasForeignKey("OrderByClientsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MyPharmacy.Entities.Drug", b =>
                {
                    b.HasOne("MyPharmacy.Entities.DrugInformation", "DrugInformation")
                        .WithMany("Drugs")
                        .HasForeignKey("DrugInformationId");

                    b.HasOne("MyPharmacy.Entities.Pharmacy", "Pharmacy")
                        .WithMany("Drugs")
                        .HasForeignKey("PharmacyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DrugInformation");

                    b.Navigation("Pharmacy");
                });

            modelBuilder.Entity("MyPharmacy.Entities.OrderByClient", b =>
                {
                    b.HasOne("MyPharmacy.Entities.Address", "Address")
                        .WithOne("OrderByClient")
                        .HasForeignKey("MyPharmacy.Entities.OrderByClient", "AddressId");

                    b.HasOne("MyPharmacy.Entities.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId");

                    b.HasOne("MyPharmacy.Entities.User", "User")
                        .WithMany("OrdersByClient")
                        .HasForeignKey("UserId");

                    b.Navigation("Address");

                    b.Navigation("Status");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MyPharmacy.Entities.Pharmacy", b =>
                {
                    b.HasOne("MyPharmacy.Entities.Address", "Address")
                        .WithOne("Pharmacy")
                        .HasForeignKey("MyPharmacy.Entities.Pharmacy", "AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("MyPharmacy.Entities.User", b =>
                {
                    b.HasOne("MyPharmacy.Entities.Pharmacy", "Pharmacy")
                        .WithMany("Users")
                        .HasForeignKey("PharmacyId");

                    b.HasOne("MyPharmacy.Entities.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pharmacy");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("MyPharmacy.Entities.Address", b =>
                {
                    b.Navigation("OrderByClient");

                    b.Navigation("Pharmacy");
                });

            modelBuilder.Entity("MyPharmacy.Entities.DrugInformation", b =>
                {
                    b.Navigation("Drugs");
                });

            modelBuilder.Entity("MyPharmacy.Entities.Pharmacy", b =>
                {
                    b.Navigation("Drugs");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("MyPharmacy.Entities.User", b =>
                {
                    b.Navigation("OrdersByClient");
                });
#pragma warning restore 612, 618
        }
    }
}
