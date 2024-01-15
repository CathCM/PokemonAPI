﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace PokemonAPI.Migrations
{
    [DbContext(typeof(PokemonDb))]
    partial class PokemonDbModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("PokemonAPI.Models.PokemonAbilityDao", b =>
                {
                    b.Property<int>("PokemonId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("AbilityName")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsHidden")
                        .HasColumnType("INTEGER");

                    b.HasKey("PokemonId", "AbilityName");

                    b.HasIndex("AbilityName");

                    b.ToTable("PokemonAbility");

                    b.HasData(
                        new
                        {
                            PokemonId = 1,
                            AbilityName = "Ability Test",
                            IsHidden = false
                        });
                });

            modelBuilder.Entity("PokemonAPI.Models.PokemonDao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Attack")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Defense")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Hp")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("SpecialAttack")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SpecialDefense")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Speed")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Pokemon");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Attack = 32,
                            Defense = 35,
                            Hp = 20,
                            Name = "Pokemon Test",
                            SpecialAttack = 51,
                            SpecialDefense = 40,
                            Speed = 15
                        });
                });

            modelBuilder.Entity("PokemonAPI.Models.TypeDao", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Name");

                    b.ToTable("Type");

                    b.HasData(
                        new
                        {
                            Name = "Type Test"
                        });
                });

            modelBuilder.Entity("PokemonTypes", b =>
                {
                    b.Property<int>("PokemonId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TypesName")
                        .HasColumnType("TEXT");

                    b.HasKey("PokemonId", "TypesName");

                    b.HasIndex("TypesName");

                    b.ToTable("PokemonTypes");

                    b.HasData(
                        new
                        {
                            PokemonId = 1,
                            TypesName = "Type Test"
                        });
                });

            modelBuilder.Entity("PokemonAPI.Models.PokemonAbilityDao", b =>
                {
                    b.HasOne("PokemonAPI.Models.AbilityDao", null)
                        .WithMany("PokemonAbility")
                        .HasForeignKey("AbilityName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PokemonAPI.Models.PokemonDao", null)
                        .WithMany("PokemonAbility")
                        .HasForeignKey("PokemonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PokemonTypes", b =>
                {
                    b.HasOne("PokemonAPI.Models.PokemonDao", null)
                        .WithMany()
                        .HasForeignKey("PokemonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PokemonAPI.Models.TypeDao", null)
                        .WithMany()
                        .HasForeignKey("TypesName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PokemonAPI.Models.AbilityDao", b =>
                {
                    b.Navigation("PokemonAbility");
                });

            modelBuilder.Entity("PokemonAPI.Models.PokemonDao", b =>
                {
                    b.Navigation("PokemonAbility");
                });
#pragma warning restore 612, 618
        }
    }
}
