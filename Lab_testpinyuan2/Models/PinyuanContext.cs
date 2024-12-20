using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Lab_testpinyuan2.Models;

public partial class PinyuanContext : DbContext
{
    public PinyuanContext()
    {
    }

    public PinyuanContext(DbContextOptions<PinyuanContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=localhost;Database=Pinyuan;Integrated Security=True;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.ToTable("Client", tb => tb.HasComment("客戶資料表"));

            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.CompanyName).HasMaxLength(30);
            entity.Property(e => e.Fax)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TaxIdnumber)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("TaxIDNumber");
            entity.Property(e => e.Tel)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK_Order");

            entity.ToTable(tb => tb.HasComment("訂單表"));

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.CompanyId).HasColumnName("CompanyID");
            entity.Property(e => e.QuoteNumber).HasMaxLength(10);
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrdeDetailId);

            entity.ToTable("Order_Detail");

            entity.Property(e => e.OrdeDetailId).HasColumnName("OrdeDetailID");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.ProductName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
