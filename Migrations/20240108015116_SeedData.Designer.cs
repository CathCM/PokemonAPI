﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace PokemonAPI.Migrations
{
    [DbContext(typeof(PokemonDb))]
    [Migration("20240108015116_SeedData")]
    partial class SeedData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0");

            modelBuilder.Entity("PokemonAPI.Models.AbilityDao", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Name");

                    b.ToTable("Ability");

                    b.HasData(
                        new
                        {
                            Name = "Ability Test"
                        });
                });

            modelBuilder.Entity("PokemonAPI.Models.PokemonDao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Pokemon");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Pokemon Test"
                        });
                });

            modelBuilder.Entity("PokemonAPI.Models.TypeDao", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Name");

                    b.ToTable("Type");
                });

            modelBuilder.Entity("PokemonAbilities", b =>
                {
                    b.Property<string>("AbilityName")
                        .HasColumnType("TEXT");

                    b.Property<int>("PokemonId")
                        .HasColumnType("INTEGER");

                    b.HasKey("AbilityName", "PokemonId");

                    b.HasIndex("PokemonId");

                    b.ToTable("PokemonAbilities");

                    b.HasData(
                        new
                        {
                            AbilityName = "Ability Test",
                            PokemonId = 1
                        });
                });

            modelBuilder.Entity("PokemonTypes", b =>
                {
                    b.Property<int>("PokemonsId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TypesName")
                        .HasColumnType("TEXT");

                    b.HasKey("PokemonsId", "TypesName");

                    b.HasIndex("TypesName");

                    b.ToTable("PokemonTypes");
                });

            modelBuilder.Entity("PokemonAbilities", b =>
                {
                    b.HasOne("PokemonAPI.Models.AbilityDao", null)
                        .WithMany()
                        .HasForeignKey("AbilityName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PokemonAPI.Models.PokemonDao", null)
                        .WithMany()
                        .HasForeignKey("PokemonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PokemonTypes", b =>
                {
                    b.HasOne("PokemonAPI.Models.PokemonDao", null)
                        .WithMany()
                        .HasForeignKey("PokemonsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PokemonAPI.Models.TypeDao", null)
                        .WithMany()
                        .HasForeignKey("TypesName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}