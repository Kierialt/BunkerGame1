﻿// <auto-generated />
using System;
using BunkerGame.Backend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BunkerGame.Backend.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.16");

            modelBuilder.Entity("BunkerGame.Backend.Models.GameRoom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int>("CurrentPlayers")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("EndedAt")
                        .HasColumnType("TEXT");

                    b.Property<int>("MaxPlayers")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("SessionCode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("StartedAt")
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Story")
                        .HasColumnType("TEXT");

                    b.Property<int>("WinnersCount")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("GameRooms");
                });

            modelBuilder.Entity("BunkerGame.Backend.Models.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AdditionalInformation")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Age")
                        .HasColumnType("INTEGER");

                    b.Property<string>("BodyType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Health")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Hobby")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Luggage")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Orientation")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Personalitie")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Phobia")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Profession")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("BunkerGame.Backend.Models.RoomPlayer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("GameRoomId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsAdditionalInfoRevealed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsAgeRevealed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsAlive")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsBodyTypeRevealed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsGenderRevealed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsHealthRevealed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsHobbyRevealed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsLuggageRevealed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsOrientationRevealed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsPersonalityRevealed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsPhobiaRevealed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsProfessionRevealed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsWinner")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("JoinedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("LeftAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<int>("PlayerId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("UserId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("GameRoomId");

                    b.HasIndex("PlayerId");

                    b.HasIndex("UserId");

                    b.ToTable("RoomPlayers");
                });

            modelBuilder.Entity("BunkerGame.Backend.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BunkerGame.Backend.Models.Vote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("VotedAt")
                        .HasColumnType("TEXT");

                    b.Property<int>("VotedForId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("VoterId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("VotingRoundId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("VotedForId");

                    b.HasIndex("VoterId");

                    b.HasIndex("VotingRoundId");

                    b.ToTable("Votes");
                });

            modelBuilder.Entity("BunkerGame.Backend.Models.VotingRound", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("EliminatedPlayerId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("EndedAt")
                        .HasColumnType("TEXT");

                    b.Property<int>("GameRoomId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RoundNumber")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("StartedAt")
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("EliminatedPlayerId");

                    b.HasIndex("GameRoomId");

                    b.ToTable("VotingRounds");
                });

            modelBuilder.Entity("BunkerGame.Backend.Models.RoomPlayer", b =>
                {
                    b.HasOne("BunkerGame.Backend.Models.GameRoom", "GameRoom")
                        .WithMany("RoomPlayers")
                        .HasForeignKey("GameRoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BunkerGame.Backend.Models.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BunkerGame.Backend.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("GameRoom");

                    b.Navigation("Player");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BunkerGame.Backend.Models.Vote", b =>
                {
                    b.HasOne("BunkerGame.Backend.Models.RoomPlayer", "VotedFor")
                        .WithMany()
                        .HasForeignKey("VotedForId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BunkerGame.Backend.Models.RoomPlayer", "Voter")
                        .WithMany()
                        .HasForeignKey("VoterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BunkerGame.Backend.Models.VotingRound", "VotingRound")
                        .WithMany("Votes")
                        .HasForeignKey("VotingRoundId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("VotedFor");

                    b.Navigation("Voter");

                    b.Navigation("VotingRound");
                });

            modelBuilder.Entity("BunkerGame.Backend.Models.VotingRound", b =>
                {
                    b.HasOne("BunkerGame.Backend.Models.RoomPlayer", "EliminatedPlayer")
                        .WithMany()
                        .HasForeignKey("EliminatedPlayerId");

                    b.HasOne("BunkerGame.Backend.Models.GameRoom", "GameRoom")
                        .WithMany("VotingRounds")
                        .HasForeignKey("GameRoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EliminatedPlayer");

                    b.Navigation("GameRoom");
                });

            modelBuilder.Entity("BunkerGame.Backend.Models.GameRoom", b =>
                {
                    b.Navigation("RoomPlayers");

                    b.Navigation("VotingRounds");
                });

            modelBuilder.Entity("BunkerGame.Backend.Models.VotingRound", b =>
                {
                    b.Navigation("Votes");
                });
#pragma warning restore 612, 618
        }
    }
}
