using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Repositories.GeneratedModels;

public partial class MyDBContext : DbContext
{
    public MyDBContext()
    {
    }

    public MyDBContext(DbContextOptions<MyDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CoronaVirusDetail> CoronaVirusDetails { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<Vaccination> Vaccinations { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    => optionsBuilder.UseNpgsql("Name=MyDBConnectionString");
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("MyDBConnectionString");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CoronaVirusDetail>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("CoronaVirusDetails_pkey");

            entity.Property(e => e.Code)
                .UseIdentityAlwaysColumn()
                .HasIdentityOptions(null, 10L, 10L, null, null, null);
            entity.Property(e => e.DateOfPositiveAnswer).HasColumnType("timestamp without time zone");
            entity.Property(e => e.DateOfRecovery).HasColumnType("timestamp without time zone");

            entity.HasOne(d => d.MemberCodeNavigation).WithMany(p => p.CoronaVirusDetails)
                .HasForeignKey(d => d.MemberCode)
                .HasConstraintName("FK_MEMBER_CODE");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("Members_pkey");

            entity.Property(e => e.Code).UseIdentityAlwaysColumn();
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.DateOfBirth).HasColumnType("timestamp without time zone");
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.HouseNumber).HasMaxLength(8);
            entity.Property(e => e.Id)
                .HasMaxLength(9)
                .HasColumnName("ID");
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.MobilePhone).HasMaxLength(10);
            entity.Property(e => e.Phone).HasMaxLength(10);
            entity.Property(e => e.Street).HasMaxLength(50);
        });

        modelBuilder.Entity<Vaccination>(entity =>
        {
            entity.HasKey(e => e.VaccinationCode).HasName("Vaccinations_pkey");

            entity.Property(e => e.VaccinationCode)
                .UseIdentityAlwaysColumn()
                .HasIdentityOptions(10L, null, null, null, null, null);
            entity.Property(e => e.DateReceived).HasColumnType("timestamp without time zone");
            entity.Property(e => e.Manufacturer).HasMaxLength(50);

            entity.HasOne(d => d.MemberCodeNavigation).WithMany(p => p.Vaccinations)
                .HasForeignKey(d => d.MemberCode)
                .HasConstraintName("FK_MEMBERCODE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
