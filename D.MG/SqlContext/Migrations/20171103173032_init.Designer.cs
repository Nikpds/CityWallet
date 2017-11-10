﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using SqlContext;
using System;

namespace SqlContext.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20171103173032_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Models.Address", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("County")
                        .IsRequired();

                    b.Property<DateTime>("LastUpdate");

                    b.Property<string>("Street")
                        .IsRequired();

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Address");
                });

            modelBuilder.Entity("Models.Bill", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Amount");

                    b.Property<string>("Bill_Id")
                        .IsRequired()
                        .HasAnnotation("unique", true);

                    b.Property<DateTime>("DateDue");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<DateTime>("LastUpdate");

                    b.Property<string>("SettlementId");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("SettlementId");

                    b.HasIndex("UserId");

                    b.ToTable("Bill");
                });

            modelBuilder.Entity("Models.Payment", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BillId")
                        .IsRequired();

                    b.Property<string>("Bill_Id")
                        .IsRequired()
                        .HasAnnotation("unique", true);

                    b.Property<DateTime>("LastUpdate");

                    b.Property<string>("Method")
                        .IsRequired();

                    b.Property<DateTime>("PaidDate");

                    b.HasKey("Id");

                    b.HasIndex("BillId")
                        .IsUnique();

                    b.ToTable("Payment");
                });

            modelBuilder.Entity("Models.Settlement", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Downpayment");

                    b.Property<int>("Installments");

                    b.Property<DateTime>("LastUpdate");

                    b.Property<DateTime>("RequestDate");

                    b.Property<string>("SettlementTypeId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("SettlementTypeId");

                    b.ToTable("Settlement");
                });

            modelBuilder.Entity("Models.SettlementType", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Downpayment");

                    b.Property<int>("Installments");

                    b.Property<double>("Interest");

                    b.Property<DateTime>("LastUpdate");

                    b.HasKey("Id");

                    b.ToTable("SettlementType");
                });

            modelBuilder.Entity("Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasAnnotation("unique", true);

                    b.Property<bool>("FirstLogin");

                    b.Property<DateTime>("LastUpdate");

                    b.Property<string>("Lastname")
                        .IsRequired();

                    b.Property<string>("Mobile");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("Vat")
                        .IsRequired()
                        .HasAnnotation("unique", true);

                    b.Property<string>("VerificationToken");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Models.Address", b =>
                {
                    b.HasOne("Models.User", "User")
                        .WithOne("Address")
                        .HasForeignKey("Models.Address", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Models.Bill", b =>
                {
                    b.HasOne("Models.Settlement", "Settlement")
                        .WithMany("Bills")
                        .HasForeignKey("SettlementId");

                    b.HasOne("Models.User", "User")
                        .WithMany("Bills")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Models.Payment", b =>
                {
                    b.HasOne("Models.Bill", "Bill")
                        .WithOne("Payment")
                        .HasForeignKey("Models.Payment", "BillId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Models.Settlement", b =>
                {
                    b.HasOne("Models.SettlementType", "SettlementType")
                        .WithMany("Settlements")
                        .HasForeignKey("SettlementTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}