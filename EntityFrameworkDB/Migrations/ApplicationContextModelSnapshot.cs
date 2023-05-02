﻿// <auto-generated />
using System;
using EntityFrameworkDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;

#nullable disable

namespace EntityFrameworkDB.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            OracleModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EntityFrameworkDB.Models.User", b =>
                {
                    b.Property<int>("idUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("idUser"));

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.HasKey("idUser");

                    b.ToTable("users");
                });

            modelBuilder.Entity("EntityFrameworkDB.Models.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ID");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("EMAIL");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TIMESTAMP(7)")
                        .HasColumnName("ENDDATA");

                    b.Property<string>("FirstName")
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("FIRST_NAME");

                    b.Property<string>("LastName")
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("LAST_NAME");

                    b.Property<string>("Password")
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("PASSWORD");

                    b.Property<int>("RoleId")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ROLEID");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TIMESTAMP(7)")
                        .HasColumnName("STARTDATA");

                    b.Property<string>("Username")
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("USERNAME");

                    b.HasKey("Id");

                    b.ToTable("USERS");
                });

            modelBuilder.Entity("EntityFrameworkDB.Models.Vacancy", b =>
                {
                    b.Property<string>("Description")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("EndDate")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<int>("Id")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("StartDate")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("TestColoumn")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("Title")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<int>("UseridUser")
                        .HasColumnType("NUMBER(10)");

                    b.HasIndex("UseridUser");

                    b.ToTable("Vacancy");
                });

            modelBuilder.Entity("EntityFrameworkDB.Models.Vacancy", b =>
                {
                    b.HasOne("EntityFrameworkDB.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UseridUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
