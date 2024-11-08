﻿// <auto-generated />
using System;
using Ass2.Model.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Ass2.Model.Migrations
{
    [DbContext(typeof(EnglishPremierLeague2024DbContext))]
    [Migration("20241015132836_update_footballPlayer")]
    partial class update_footballPlayer
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Ass2.Model.Models.FootballClub", b =>
                {
                    b.Property<string>("FootballClubId")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("FootballClubID");

                    b.Property<string>("ClubName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ClubShortDescription")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<string>("Mascos")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("SoccerPracticeField")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("FootballClubId")
                        .HasName("PK__Football__9127950498A46AB5");

                    b.ToTable("FootballClub", (string)null);
                });

            modelBuilder.Entity("Ass2.Model.Models.FootballPlayer", b =>
                {
                    b.Property<string>("FootballPlayerId")
                        .HasMaxLength(36)
                        .HasColumnType("nvarchar(36)")
                        .HasColumnName("FootballPlayerID");

                    b.Property<string>("Achievements")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<DateTime?>("Birthday")
                        .HasColumnType("datetime");

                    b.Property<string>("FootballClubId")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("FootballClubID");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Nomination")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.Property<string>("PlayerExperiences")
                        .IsRequired()
                        .HasMaxLength(400)
                        .HasColumnType("nvarchar(400)");

                    b.HasKey("FootballPlayerId")
                        .HasName("PK__Football__6D5466C3E2FE101F");

                    b.HasIndex("FootballClubId");

                    b.ToTable("FootballPlayer", (string)null);
                });

            modelBuilder.Entity("Ass2.Model.Models.PremierLeagueAccount", b =>
                {
                    b.Property<int>("AccId")
                        .HasColumnType("int")
                        .HasColumnName("AccID");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(140)
                        .HasColumnType("nvarchar(140)");

                    b.Property<string>("EmailAddress")
                        .HasMaxLength(90)
                        .HasColumnType("nvarchar(90)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(90)
                        .HasColumnType("nvarchar(90)");

                    b.Property<int?>("Role")
                        .HasColumnType("int");

                    b.HasKey("AccId")
                        .HasName("PK__PremierL__91CBC3988138D158");

                    b.HasIndex(new[] { "EmailAddress" }, "UQ__PremierL__49A14740704BF9BC")
                        .IsUnique()
                        .HasFilter("[EmailAddress] IS NOT NULL");

                    b.ToTable("PremierLeagueAccount", (string)null);
                });

            modelBuilder.Entity("Ass2.Model.Models.FootballPlayer", b =>
                {
                    b.HasOne("Ass2.Model.Models.FootballClub", "FootballClub")
                        .WithMany("FootballPlayers")
                        .HasForeignKey("FootballClubId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK__FootballP__Footb__3C69FB99");

                    b.Navigation("FootballClub");
                });

            modelBuilder.Entity("Ass2.Model.Models.FootballClub", b =>
                {
                    b.Navigation("FootballPlayers");
                });
#pragma warning restore 612, 618
        }
    }
}
