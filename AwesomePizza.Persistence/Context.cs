using System;
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
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
