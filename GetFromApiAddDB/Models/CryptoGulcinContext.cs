using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GetFromApiAddDB.Models;

public partial class CryptoGulcinContext : DbContext
{
    public CryptoGulcinContext()
    {
    }

    public CryptoGulcinContext(DbContextOptions<CryptoGulcinContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Currency> Currencies { get; set; }

    public virtual DbSet<CurrencyAction> CurrencyActions { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=DESKTOP-C13QDLJ\\MSSQLSERVER01;Database=CryptoGulcin;Trusted_Connection=True;encrypt=false;");





    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Currency>(entity =>
        {
            entity.ToTable("Currency");

            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Symbol).HasMaxLength(50);
        });

        modelBuilder.Entity<CurrencyAction>(entity =>
        {
            entity.ToTable("CurrencyAction");

            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.Price).HasColumnType("money");


            //burayi silebilrim
            //entity.HasOne(d => d.Currency).WithMany(p => p.CurrencyActions)
            //    .HasForeignKey(d => d.CurrencyId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_CurrencyAction_Currency");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
