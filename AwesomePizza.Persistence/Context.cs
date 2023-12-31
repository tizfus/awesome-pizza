﻿using System;
using AwesomePizza.Persistence.Entity;
using Microsoft.EntityFrameworkCore;

namespace AwesomePizza.Persistence;

public partial class Context : DbContext
{
    public DbSet<Order> Orders { get; set; }

    public Context(DbContextOptions<Context> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
        modelBuilder.Entity<Order>().Property(entity => entity.Id)
            .HasColumnName("id")
            .IsRequired();

        modelBuilder.Entity<Order>().Property(entity => entity.Status)
            .HasColumnName("status")
            .IsRequired();

        modelBuilder.Entity<Order>().Property(entity => entity.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
