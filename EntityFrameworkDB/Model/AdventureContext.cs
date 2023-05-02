using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkDB.Model;

public partial class AdventureContext : DbContext
{
    public AdventureContext()
    {
    }

    public AdventureContext(DbContextOptions<AdventureContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Logg> Loggs { get; set; }

    public virtual DbSet<Match> Matches { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<PlayerGoal> PlayerGoals { get; set; }

    public virtual DbSet<Prognose> Prognoses { get; set; }

    public virtual DbSet<PrognoseHistory> PrognoseHistories { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<Voter> Voters { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseOracle("Data Source=192.168.105.240:1521/SDBdev;Persist Security Info=True;User ID=IJOSU;Password=FWC123asd2");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("IJOSU");

        modelBuilder.Entity<Logg>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C0025875");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Loggdate).HasDefaultValueSql("SYSDATE\n");
        });

        modelBuilder.Entity<Match>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C0025831");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasOne(d => d.FirstTeam).WithMany(p => p.MatchFirstTeams).HasConstraintName("FK_MATCHTF");

            entity.HasOne(d => d.SecondTeam).WithMany(p => p.MatchSecondTeams).HasConstraintName("FK_MATCHTS");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C0025841");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasOne(d => d.Team).WithMany(p => p.Players).HasConstraintName("FK_PLAYERST");
        });

        modelBuilder.Entity<PlayerGoal>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C0025843");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasOne(d => d.Match).WithMany(p => p.PlayerGoals).HasConstraintName("FK_PLAYERGOALSM");

            entity.HasOne(d => d.Player).WithMany(p => p.PlayerGoals).HasConstraintName("FK_PLAYERGOALSP");
        });

        modelBuilder.Entity<Prognose>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C0025836");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.PrognosedDate).HasDefaultValueSql("SYSDATE");

            entity.HasOne(d => d.Match).WithMany(p => p.Prognoses).HasConstraintName("FK_PROGNOSEM");

            entity.HasOne(d => d.PrognosedPlayerNavigation).WithMany(p => p.Prognoses).HasConstraintName("FK_PROGNOSEP");

            entity.HasOne(d => d.PrognosedTeamNavigation).WithMany(p => p.Prognoses).HasConstraintName("FK_PROGNOSET");

            entity.HasOne(d => d.Voter).WithMany(p => p.Prognoses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROGNOSEV");
        });

        modelBuilder.Entity<PrognoseHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C0025839");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.PrognosedDate).HasDefaultValueSql("SYSDATE");

            entity.HasOne(d => d.Match).WithMany(p => p.PrognoseHistories).HasConstraintName("FK_PROGNOSE_HISTORYM");

            entity.HasOne(d => d.PrognosedPlayerNavigation).WithMany(p => p.PrognoseHistories).HasConstraintName("FK_PROGNOSE_HISTORYP");

            entity.HasOne(d => d.PrognosedTeamNavigation).WithMany(p => p.PrognoseHistories).HasConstraintName("FK_PROGNOSE_HISTORYT");

            entity.HasOne(d => d.Voter).WithMany(p => p.PrognoseHistories)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PROGNOSE_HISTORYV");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C0025833");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Voter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C0025862");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasOne(d => d.VotedTeamNavigation).WithMany(p => p.Voters).HasConstraintName("FK_VOTERT");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
