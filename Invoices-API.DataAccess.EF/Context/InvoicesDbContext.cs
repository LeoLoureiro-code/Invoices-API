using System;
using System.Collections.Generic;
using Invoices_API.DataAccess.EF.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace Invoices_API.DataAccess.EF.Context;

public partial class InvoicesDbContext : DbContext
{
    public InvoicesDbContext()
    {
    }

    public InvoicesDbContext(DbContextOptions<InvoicesDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<ItemList> ItemLists { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8_unicode_ci")
            .HasCharSet("utf8");

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Invoice");

            entity.HasIndex(e => e.UserId, "UserId_idx");

            entity.Property(e => e.Id).HasColumnType("int(10) unsigned");
            entity.Property(e => e.City).HasMaxLength(45);
            entity.Property(e => e.ClientCity)
                .HasMaxLength(45)
                .HasColumnName("Client_city");
            entity.Property(e => e.ClientCountry)
                .HasMaxLength(45)
                .HasColumnName("Client_country");
            entity.Property(e => e.ClientEmail)
                .HasMaxLength(45)
                .HasColumnName("Client_email");
            entity.Property(e => e.ClientName)
                .HasMaxLength(45)
                .HasColumnName("Client_name");
            entity.Property(e => e.ClientPostCode)
                .HasMaxLength(45)
                .HasColumnName("Client_post_code");
            entity.Property(e => e.ClientStreet)
                .HasMaxLength(250)
                .HasColumnName("Client_street");
            entity.Property(e => e.Country).HasMaxLength(45);
            entity.Property(e => e.InvoiceDate).HasColumnName("Invoice_date");
            entity.Property(e => e.InvoicePayment).HasColumnName("Invoice_payment");
            entity.Property(e => e.PostCode)
                .HasMaxLength(45)
                .HasColumnName("Post_code");
            entity.Property(e => e.ProjectDescription)
                .HasMaxLength(200)
                .HasColumnName("Project_description");
            entity.Property(e => e.Street).HasMaxLength(250);
            entity.Property(e => e.UserId).HasColumnType("int(11)");

            entity.HasOne(d => d.User).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("UserId");
        });

        modelBuilder.Entity<ItemList>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Item_list");

            entity.HasIndex(e => e.InvoiceId, "Invoice_id_idx");

            entity.Property(e => e.Id).HasColumnType("int(10) unsigned");
            entity.Property(e => e.InvoiceId)
                .HasColumnType("int(10) unsigned")
                .HasColumnName("Invoice_id");
            entity.Property(e => e.Name).HasMaxLength(45);
            entity.Property(e => e.Price).HasColumnType("int(10) unsigned");
            entity.Property(e => e.Quantity).HasColumnType("int(10) unsigned");

            entity.HasOne(d => d.Invoice).WithMany(p => p.ItemLists)
                .HasForeignKey(d => d.InvoiceId)
                .HasConstraintName("Invoice_id");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Email).HasMaxLength(45);
            entity.Property(e => e.Password).HasMaxLength(265);
            entity.Property(e => e.Role).HasMaxLength(45);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
