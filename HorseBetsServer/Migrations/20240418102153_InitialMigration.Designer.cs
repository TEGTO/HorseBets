﻿// <auto-generated />
using System;
using HorseBets.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HorseBets.Migrations
{
    [DbContext(typeof(BetsDbContext))]
    [Migration("20240418102153_InitialMigration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("HorseBets.Bets.Models.Bet", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text");

                    b.Property<decimal>("BetAmount")
                        .HasColumnType("numeric");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("HorseId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("MatchId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("HorseId");

                    b.HasIndex("MatchId");

                    b.ToTable("Bets");
                });

            modelBuilder.Entity("HorseBets.Bets.Models.Client", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text");

                    b.Property<decimal>("Balance")
                        .HasColumnType("numeric");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("HorseBets.Bets.Models.Horse", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<float>("Speed")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.ToTable("Horses");
                });

            modelBuilder.Entity("HorseBets.Bets.Models.HorseCoefficient", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text");

                    b.Property<float>("Coefficient")
                        .HasColumnType("real");

                    b.Property<string>("HorseId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("MatchId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("HorseId");

                    b.HasIndex("MatchId");

                    b.ToTable("HorseCoefficients");
                });

            modelBuilder.Entity("HorseBets.Bets.Models.HorseMatchWinner", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text");

                    b.Property<string>("HorseId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("MatchId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("HorseId");

                    b.HasIndex("MatchId")
                        .IsUnique();

                    b.ToTable("HorseMatchWinner");
                });

            modelBuilder.Entity("HorseBets.Bets.Models.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("HorseMatch", b =>
                {
                    b.Property<int>("MatchesId")
                        .HasColumnType("integer");

                    b.Property<string>("ParticipantsId")
                        .HasColumnType("text");

                    b.HasKey("MatchesId", "ParticipantsId");

                    b.HasIndex("ParticipantsId");

                    b.ToTable("HorseMatch");
                });

            modelBuilder.Entity("HorseBets.Bets.Models.Bet", b =>
                {
                    b.HasOne("HorseBets.Bets.Models.Client", "Client")
                        .WithMany()
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HorseBets.Bets.Models.Horse", "Horse")
                        .WithMany()
                        .HasForeignKey("HorseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HorseBets.Bets.Models.Match", "Match")
                        .WithMany()
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Horse");

                    b.Navigation("Match");
                });

            modelBuilder.Entity("HorseBets.Bets.Models.HorseCoefficient", b =>
                {
                    b.HasOne("HorseBets.Bets.Models.Horse", "Horse")
                        .WithMany()
                        .HasForeignKey("HorseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HorseBets.Bets.Models.Match", "Match")
                        .WithMany()
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Horse");

                    b.Navigation("Match");
                });

            modelBuilder.Entity("HorseBets.Bets.Models.HorseMatchWinner", b =>
                {
                    b.HasOne("HorseBets.Bets.Models.Horse", "Horse")
                        .WithMany()
                        .HasForeignKey("HorseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HorseBets.Bets.Models.Match", "Match")
                        .WithOne("Winner")
                        .HasForeignKey("HorseBets.Bets.Models.HorseMatchWinner", "MatchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Horse");

                    b.Navigation("Match");
                });

            modelBuilder.Entity("HorseMatch", b =>
                {
                    b.HasOne("HorseBets.Bets.Models.Match", null)
                        .WithMany()
                        .HasForeignKey("MatchesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HorseBets.Bets.Models.Horse", null)
                        .WithMany()
                        .HasForeignKey("ParticipantsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HorseBets.Bets.Models.Match", b =>
                {
                    b.Navigation("Winner");
                });
#pragma warning restore 612, 618
        }
    }
}
