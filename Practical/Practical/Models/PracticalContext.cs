using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Practical.Models;

public partial class PracticalContext : DbContext
{
    public PracticalContext()
    {
    }

    public PracticalContext(DbContextOptions<PracticalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Car> Cars { get; set; }

    public virtual DbSet<Model> Models { get; set; }

    public virtual DbSet<Version> Versions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("data source=.;initial catalog=Practical;user id=sa;password=aptech; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.CarId).HasName("PK__Cars__68A0340E2628DE89");

            entity.Property(e => e.CarId).HasColumnName("CarID");
            entity.Property(e => e.Brand).HasMaxLength(100);
            entity.Property(e => e.VersionId).HasColumnName("VersionID");

            entity.HasOne(d => d.Version).WithMany(p => p.Cars)
                .HasForeignKey(d => d.VersionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Cars__VersionID__286302EC");
        });

        modelBuilder.Entity<Model>(entity =>
        {
            entity.HasKey(e => e.ModelId).HasName("PK__Models__E8D7A1CC9D955440");

            entity.Property(e => e.ModelId).HasColumnName("ModelID");
            entity.Property(e => e.ModelName).HasMaxLength(100);
            entity.Property(e => e.Type).HasMaxLength(50);
        });

        modelBuilder.Entity<Version>(entity =>
        {
            entity.HasKey(e => e.VersionId).HasName("PK__Versions__16C6402F2AC39B9E");

            entity.Property(e => e.VersionId).HasColumnName("VersionID");
            entity.Property(e => e.EngineType).HasMaxLength(50);
            entity.Property(e => e.ModelId).HasColumnName("ModelID");
            entity.Property(e => e.VersionName).HasMaxLength(100);

            entity.HasOne(d => d.Model).WithMany(p => p.Versions)
                .HasForeignKey(d => d.ModelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Versions__ModelI__25869641");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
