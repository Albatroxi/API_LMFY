﻿// <auto-generated />
using API_LMFY.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API_LMFY.Migrations
{
    [DbContext(typeof(APIContextoDB))]
    [Migration("20240504211253_Inicial")]
    partial class Inicial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("API_LMFY.Models.users.users", b =>
                {
                    b.Property<string>("email")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("attribute")
                        .HasColumnType("int");

                    b.Property<string>("nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("pssW")
                        .IsRequired()
                        .HasMaxLength(2147483647)
                        .HasColumnType("longtext");

                    b.HasKey("email");

                    b.ToTable("users");
                });
#pragma warning restore 612, 618
        }
    }
}
