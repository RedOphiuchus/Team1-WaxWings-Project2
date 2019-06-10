using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Data.Entities
{
    public partial class HLContext : DbContext
    {
        public HLContext()
        {
        }

        public HLContext(DbContextOptions<HLContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Challenge> Challenge { get; set; }
        public virtual DbSet<DirectMessage> DirectMessage { get; set; }
        public virtual DbSet<GameModes> GameModes { get; set; }
        public virtual DbSet<Rank> Rank { get; set; }
        public virtual DbSet<Sides> Sides { get; set; }
        public virtual DbSet<Team> Team { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserTeam> UserTeam { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                using (StreamReader read = new StreamReader("../../../../PizzaBoxData/ConnectionString.txt"))
                {
                    optionsBuilder.UseSqlServer(read.ReadLine()/*"Server = sera-server.database.windows.net; Database = HeavensLadder; user id = redophiuchus; Password = Password1;"*/);
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Challenge>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.GameModeId).HasColumnName("gameModeId");

                entity.HasOne(d => d.GameMode)
                    .WithMany(p => p.Challenge)
                    .HasForeignKey(d => d.GameModeId)
                    .HasConstraintName("FK__Challenge__gameM__5FB337D6");
            });

            modelBuilder.Entity<DirectMessage>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Messagetime)
                    .HasColumnName("messagetime")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Recieveid).HasColumnName("recieveid");

                entity.Property(e => e.Sendid).HasColumnName("sendid");

                entity.HasOne(d => d.Recieve)
                    .WithMany(p => p.DirectMessageRecieve)
                    .HasForeignKey(d => d.Recieveid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DirectMes__recie__5535A963");

                entity.HasOne(d => d.Send)
                    .WithMany(p => p.DirectMessageSend)
                    .HasForeignKey(d => d.Sendid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DirectMes__sendi__5441852A");
            });

            modelBuilder.Entity<GameModes>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Modename)
                    .IsRequired()
                    .HasColumnName("modename")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Rank>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Gamemodeid).HasColumnName("gamemodeid");

                entity.Property(e => e.Losses).HasColumnName("losses");

                entity.Property(e => e.Rank1).HasColumnName("rank");

                entity.Property(e => e.Teamid).HasColumnName("teamid");

                entity.Property(e => e.Wins).HasColumnName("wins");

                entity.HasOne(d => d.Gamemode)
                    .WithMany(p => p.Rank)
                    .HasForeignKey(d => d.Gamemodeid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Rank__gamemodeid__5DCAEF64");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.Rank)
                    .HasForeignKey(d => d.Teamid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Rank__teamid__5EBF139D");
            });

            modelBuilder.Entity<Sides>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Challengeid).HasColumnName("challengeid");

                entity.Property(e => e.Teamid).HasColumnName("teamid");

                entity.Property(e => e.Winreport).HasColumnName("winreport");

                entity.HasOne(d => d.Challenge)
                    .WithMany(p => p.Sides)
                    .HasForeignKey(d => d.Challengeid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Sides__challenge__5812160E");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.Sides)
                    .HasForeignKey(d => d.Teamid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Sides__teamid__59063A47");
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Teamname)
                    .IsRequired()
                    .HasColumnName("teamname")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserTeam>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Leader).HasColumnName("leader");

                entity.Property(e => e.Teamid).HasColumnName("teamid");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.UserTeam)
                    .HasForeignKey(d => d.Teamid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserTeam__teamid__4F7CD00D");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserTeam)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__UserTeam__userid__5070F446");
            });
        }
    }
}
