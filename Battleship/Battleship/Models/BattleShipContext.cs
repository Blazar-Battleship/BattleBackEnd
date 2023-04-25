using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Battleship.Models;

public partial class BattleShipContext : DbContext
{
    public BattleShipContext()
    {
    }

    public BattleShipContext(DbContextOptions<BattleShipContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Coalition> Coalitions { get; set; }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<Grid> Grids { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<Ship> Ships { get; set; }

    public virtual DbSet<ShipSlice> ShipSlices { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=USERNZX-HGG8ASN;Initial Catalog=BattleShip;Integrated Security=True;Persist Security Info=True;User ID=sa;Password='wwr200261';Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Coalition>(entity =>
        {
            entity.ToTable("Coalition");

            entity.Property(e => e.Name)
                .HasMaxLength(10)
                .IsFixedLength();

            entity.HasOne(d => d.Game).WithMany(p => p.Coalitions)
                .HasForeignKey(d => d.GameId)
                .HasConstraintName("FK_Coalition_Game");
        });

        modelBuilder.Entity<Game>(entity =>
        {
            entity.ToTable("Game");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Grid>(entity =>
        {
            entity.ToTable("Grid");

            entity.HasOne(d => d.Coalition).WithMany(p => p.Grids)
                .HasForeignKey(d => d.CoalitionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Grid_Coalition");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.ToTable("Player");

            entity.Property(e => e.Name)
                .HasMaxLength(10)
                .IsFixedLength();

            entity.HasOne(d => d.Coalition).WithMany(p => p.Players)
                .HasForeignKey(d => d.CoalitionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Player_Coalition");
        });

        modelBuilder.Entity<Ship>(entity =>
        {
            entity.ToTable("Ship");

            entity.HasOne(d => d.Grid).WithMany(p => p.Ships)
                .HasForeignKey(d => d.GridId)
                .HasConstraintName("FK_Ship_Grid");
        });

        modelBuilder.Entity<ShipSlice>(entity =>
        {
            entity.ToTable("ShipSlice");

            entity.Property(e => e.Team)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.X).HasColumnName("x");
            entity.Property(e => e.Y).HasColumnName("y");

            entity.HasOne(d => d.Ship).WithMany(p => p.ShipSlices)
                .HasForeignKey(d => d.ShipId)
                .HasConstraintName("FK_ShipSlice_Ship");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
