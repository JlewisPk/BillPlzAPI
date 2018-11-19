﻿// <auto-generated />
using BillPlzAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BillPlzAPI.Migrations
{
    [DbContext(typeof(BillPlzAPIContext))]
    [Migration("20181119123929_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024");

            modelBuilder.Entity("BillPlzAPI.Models.ItemObject", b =>
                {
                    b.Property<int>("itemId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("itemCount");

                    b.Property<string>("itemName");

                    b.Property<int>("itemPrice");

                    b.Property<string>("itemURL");

                    b.HasKey("itemId");

                    b.ToTable("ItemObject");
                });
#pragma warning restore 612, 618
        }
    }
}
