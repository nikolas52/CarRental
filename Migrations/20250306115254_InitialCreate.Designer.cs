﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Wypozyczalnia.Data;

#nullable disable

namespace ProjPM.Migrations
{
    [DbContext(typeof(WypozyczalniaContext))]
    [Migration("20250306115254_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Wypozyczalnia.Models.Klient", b =>
                {
                    b.Property<int>("IdKlienta")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdKlienta"));

                    b.Property<string>("Adres")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Imie")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nazwisko")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefon")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdKlienta");

                    b.ToTable("Klienci", (string)null);
                });

            modelBuilder.Entity("Wypozyczalnia.Models.Samochod", b =>
                {
                    b.Property<int>("IdSamochodu")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdSamochodu"));

                    b.Property<int>("CenaZaWypozyczenie")
                        .HasColumnType("int");

                    b.Property<bool>("Dostepnosc")
                        .HasColumnType("bit");

                    b.Property<string>("Kolor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Marka")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MocSilnika")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PojSilnika")
                        .HasColumnType("int");

                    b.Property<int>("RokProdukcji")
                        .HasColumnType("int");

                    b.HasKey("IdSamochodu");

                    b.ToTable("Samochody", (string)null);
                });

            modelBuilder.Entity("Wypozyczalnia.Models.Wypozyczenie", b =>
                {
                    b.Property<int>("IdWypozyczenia")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdWypozyczenia"));

                    b.Property<DateTime>("DataRozpoczecia")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataZakonczenia")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdKlienta")
                        .HasColumnType("int");

                    b.Property<int>("IdSamochodu")
                        .HasColumnType("int");

                    b.Property<int>("Kwota")
                        .HasColumnType("int");

                    b.HasKey("IdWypozyczenia");

                    b.HasIndex("IdKlienta");

                    b.HasIndex("IdSamochodu");

                    b.ToTable("Wypozyczenia", (string)null);
                });

            modelBuilder.Entity("Wypozyczalnia.Models.Wypozyczenie", b =>
                {
                    b.HasOne("Wypozyczalnia.Models.Klient", "Klient")
                        .WithMany("Wypozyczenia")
                        .HasForeignKey("IdKlienta")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Wypozyczalnia.Models.Samochod", "Samochod")
                        .WithMany("Wypozyczenia")
                        .HasForeignKey("IdSamochodu")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Klient");

                    b.Navigation("Samochod");
                });

            modelBuilder.Entity("Wypozyczalnia.Models.Klient", b =>
                {
                    b.Navigation("Wypozyczenia");
                });

            modelBuilder.Entity("Wypozyczalnia.Models.Samochod", b =>
                {
                    b.Navigation("Wypozyczenia");
                });
#pragma warning restore 612, 618
        }
    }
}
