﻿// <auto-generated />
using System;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data.Migrations
{
    [DbContext(typeof(HLContext))]
    partial class HLContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Data.Entities.Challenge", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("GameModeId")
                        .HasColumnName("gameModeId");

                    b.HasKey("Id");

                    b.HasIndex("GameModeId");

                    b.ToTable("Challenge");
                });

            modelBuilder.Entity("Data.Entities.DirectMessage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("Messagetime")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("messagetime")
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<int>("Recieveid")
                        .HasColumnName("recieveid");

                    b.Property<int>("Sendid")
                        .HasColumnName("sendid");

                    b.HasKey("Id");

                    b.HasIndex("Recieveid");

                    b.HasIndex("Sendid");

                    b.ToTable("DirectMessage");
                });

            modelBuilder.Entity("Data.Entities.GameModes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Modename")
                        .IsRequired()
                        .HasColumnName("modename")
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("GameModes");
                });

            modelBuilder.Entity("Data.Entities.Rank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Gamemodeid")
                        .HasColumnName("gamemodeid");

                    b.Property<int>("Losses")
                        .HasColumnName("losses");

                    b.Property<int>("Rank1")
                        .HasColumnName("rank");

                    b.Property<int>("Teamid")
                        .HasColumnName("teamid");

                    b.Property<int>("Wins")
                        .HasColumnName("wins");

                    b.HasKey("Id");

                    b.HasIndex("Gamemodeid");

                    b.HasIndex("Teamid");

                    b.ToTable("Rank");
                });

            modelBuilder.Entity("Data.Entities.Sides", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Challengeid")
                        .HasColumnName("challengeid");

                    b.Property<int>("Teamid")
                        .HasColumnName("teamid");

                    b.Property<bool?>("Winreport")
                        .HasColumnName("winreport");

                    b.HasKey("Id");

                    b.HasIndex("Challengeid");

                    b.HasIndex("Teamid");

                    b.ToTable("Sides");
                });

            modelBuilder.Entity("Data.Entities.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Teamname")
                        .IsRequired()
                        .HasColumnName("teamname")
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("Team");
                });

            modelBuilder.Entity("Data.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnName("password")
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnName("username")
                        .HasMaxLength(30)
                        .IsUnicode(false);

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Data.Entities.UserTeam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Leader")
                        .HasColumnName("leader");

                    b.Property<int>("Teamid")
                        .HasColumnName("teamid");

                    b.Property<int>("Userid")
                        .HasColumnName("userid");

                    b.HasKey("Id");

                    b.HasIndex("Teamid");

                    b.HasIndex("Userid");

                    b.ToTable("UserTeam");
                });

            modelBuilder.Entity("Data.Entities.Challenge", b =>
                {
                    b.HasOne("Data.Entities.GameModes", "GameMode")
                        .WithMany("Challenge")
                        .HasForeignKey("GameModeId")
                        .HasConstraintName("FK__Challenge__gameM__5FB337D6");
                });

            modelBuilder.Entity("Data.Entities.DirectMessage", b =>
                {
                    b.HasOne("Data.Entities.User", "Recieve")
                        .WithMany("DirectMessageRecieve")
                        .HasForeignKey("Recieveid")
                        .HasConstraintName("FK__DirectMes__recie__5535A963");

                    b.HasOne("Data.Entities.User", "Send")
                        .WithMany("DirectMessageSend")
                        .HasForeignKey("Sendid")
                        .HasConstraintName("FK__DirectMes__sendi__5441852A");
                });

            modelBuilder.Entity("Data.Entities.Rank", b =>
                {
                    b.HasOne("Data.Entities.GameModes", "Gamemode")
                        .WithMany("Rank")
                        .HasForeignKey("Gamemodeid")
                        .HasConstraintName("FK__Rank__gamemodeid__5DCAEF64");

                    b.HasOne("Data.Entities.Team", "Team")
                        .WithMany("Rank")
                        .HasForeignKey("Teamid")
                        .HasConstraintName("FK__Rank__teamid__5EBF139D");
                });

            modelBuilder.Entity("Data.Entities.Sides", b =>
                {
                    b.HasOne("Data.Entities.Challenge", "Challenge")
                        .WithMany("Sides")
                        .HasForeignKey("Challengeid")
                        .HasConstraintName("FK__Sides__challenge__5812160E");

                    b.HasOne("Data.Entities.Team", "Team")
                        .WithMany("Sides")
                        .HasForeignKey("Teamid")
                        .HasConstraintName("FK__Sides__teamid__59063A47");
                });

            modelBuilder.Entity("Data.Entities.UserTeam", b =>
                {
                    b.HasOne("Data.Entities.Team", "Team")
                        .WithMany("UserTeam")
                        .HasForeignKey("Teamid")
                        .HasConstraintName("FK__UserTeam__teamid__4F7CD00D");

                    b.HasOne("Data.Entities.User", "User")
                        .WithMany("UserTeam")
                        .HasForeignKey("Userid")
                        .HasConstraintName("FK__UserTeam__userid__5070F446");
                });
#pragma warning restore 612, 618
        }
    }
}
