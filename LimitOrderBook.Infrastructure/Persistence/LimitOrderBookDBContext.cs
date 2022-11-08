using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LimitOrderBook.Infrastructure.Persistence.Models;

namespace LimitOrderBook.Infrastructure.Persistence;

public class LimitOrderBookDbContext : DbContext
{
    
    public LimitOrderBookDbContext(DbContextOptions options) : base(options)
    {

    }  

    public DbSet<StockModel> Stocks { get; set; } = null!;

    public DbSet<PositionModel> Positions { get; set; } = null!;

    public DbSet<SaleModel> Sales { get; set; } = null!;

    public DbSet<UserModel> Users { get; set; } = null!;

    public DbSet<OrderModel> Orders { get; set; } = null!;

    public DbSet<PortfolioModel> Portfolios { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderModel>().Navigation(o => o.issuer).AutoInclude();
        modelBuilder.Entity<OrderModel>().Navigation(o => o.issuer).AutoInclude();
        modelBuilder.Entity<OrderModel>().Navigation(o => o.underlying).AutoInclude();
        modelBuilder.Entity<UserModel>().Navigation(u => u.portfolio).AutoInclude();
        modelBuilder.Entity<PortfolioModel>().Navigation(p => p.positions).AutoInclude();
        modelBuilder.Entity<PositionModel>().Navigation(p => p.underlying).AutoInclude();
        modelBuilder.Entity<SaleModel>().Navigation(s => s.buyer).AutoInclude();
        modelBuilder.Entity<SaleModel>().Navigation(s => s.seller).AutoInclude();
        modelBuilder.Entity<SaleModel>().Navigation(s => s.underlying).AutoInclude();

    }

    /*
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseSqlServer(@"Data Source=INB220701\SQLSERVERLEON; Initial Catalog=LimitOrderBookDB;Integrated Security=True;");
    }  */
}
