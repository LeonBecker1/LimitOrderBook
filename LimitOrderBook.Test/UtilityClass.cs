using AutoMapper;
using LimitOrderBook.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LimitOrderBook.Infrastructure.Persistence.Models;
using LimitOrderBook.Application.Exceptions;
using LimitOrderBook.Domain.Entities;
using LimitOrderBook.Infrastructure.Services.Matching;
using Microsoft.AspNetCore.SignalR.Client;
using LimitOrderBook.Infrastructure.Hubs;
using LimitOrderBook.Infrastructure.Services.Authentication;
using Microsoft.Extensions.Options;
using LimitOrderBook.Infrastructure.Options;

namespace LimitOrderBook.Test;

public static class UtilityClass
{

    private static LimitOrderBookDbContext context = null!;

    private static LimitOrderBookDbContext persistentContext = null!;
    
    public static async Task<LimitOrderBookDbContext> GetPersistentDbContext()
    {
        if(persistentContext is null)
        {
            var options = new DbContextOptionsBuilder<LimitOrderBookDbContext>()
            .UseSqlServer(@"Data Source=INB220701\SQLSERVERLEON; Initial Catalog=LimitOrderBookDB;Integrated Security=True;")
            .Options;
            //LimitOrderBookDbContext databaseContext = null!; 
            var databaseContext = new LimitOrderBookDbContext(options);
            await databaseContext.SaveChangesAsync();
            persistentContext = databaseContext;
            return databaseContext;
        }
        else
        {
            return persistentContext;
        }
        
    }

    public static async Task<LimitOrderBookDbContext> GetDbContext()
    {
        if(context is null)
        {
            var options = new DbContextOptionsBuilder<LimitOrderBookDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
            //LimitOrderBookDbContext databaseContext = null!;
            var databaseContext = new LimitOrderBookDbContext(options);
            await databaseContext.SaveChangesAsync();
            context = databaseContext;
            return databaseContext;
        }
        else
        {
            return context;
        }
        
    }

    public static IMapper GetMapper()
    {
        MapperConfiguration mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new AutoMapperProfile());
        });
        return mockMapper.CreateMapper();
    }

    public static async Task<PositionRepository> GetPositionRepository()
    {
        LimitOrderBookDbContext context = await GetDbContext();
        IMapper mapper = GetMapper();
        return new PositionRepository(context, mapper);

    }

    public static async Task<PortfolioRepository> GetPortfolioRepository()
    {
        LimitOrderBookDbContext context = await GetDbContext();
        IMapper mapper = GetMapper();
        return new PortfolioRepository(context, mapper);

    }

    public static async Task<OrderRepository> GetOrderRepository()
    {
        LimitOrderBookDbContext context = await GetDbContext();
        IMapper mapper = GetMapper();
        return new OrderRepository(context, mapper);

    }

    public static async Task<SaleRepository> GetSaleRepository()
    {
        LimitOrderBookDbContext context = await GetDbContext();
        IMapper mapper = GetMapper();
        return new SaleRepository(context, mapper);
    }

    public static async Task<StockRepository> GetStockRepository()
    {
        LimitOrderBookDbContext context = await GetDbContext();
        IMapper mapper = GetMapper();
        return new StockRepository(context, mapper);

    }

    public static async Task<UserRepository> GetUserRepository()
    {
        var context = await GetDbContext();
        IMapper mapper = GetMapper();
        return new UserRepository(context, mapper);

    }

    public static async Task<PositionRepository> GetPersistentPositionRepository()
    {
        LimitOrderBookDbContext context = await GetPersistentDbContext();
        IMapper mapper = GetMapper();
        return new PositionRepository(context, mapper);

    }

    public static async Task<PortfolioRepository> GetPersistentPortfolioRepository()
    {
        LimitOrderBookDbContext context = await GetPersistentDbContext();
        IMapper mapper = GetMapper();
        return new PortfolioRepository(context, mapper);

    }

    public static async Task<OrderRepository> GetPersistentOrderRepository()
    {
        LimitOrderBookDbContext context = await GetPersistentDbContext();
        IMapper mapper = GetMapper();
        return new OrderRepository(context, mapper);

    }

    public static async Task<SaleRepository> GetPersistentSaleRepository()
    {
        LimitOrderBookDbContext context = await GetPersistentDbContext();
        IMapper mapper = GetMapper();
        return new SaleRepository(context, mapper);
    }

    public static async Task<StockRepository> GetPersistentStockRepository()
    {
        LimitOrderBookDbContext context = await GetPersistentDbContext();
        IMapper mapper = GetMapper();
        return new StockRepository(context, mapper);

    }

    public static async Task<UserRepository> GetPersistentUserRepository()
    {
        var context = await GetPersistentDbContext();
        IMapper mapper = GetMapper();
        return new UserRepository(context, mapper);

    }

    public static async Task<UnitofWork> GetUnitofWork()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<AutoMapperProfile>();
        });

        IMapper mapper = config.CreateMapper();
        DbContext dbContext = await GetDbContext();
        return new UnitofWork(dbContext, mapper);

    }

    public static async Task<MatchingEgine> GetMatchingEngine()
    {
        var unitofWork = await GetUnitofWork();

        MockHubConnectionWrapper mockHub = new();

        return new MatchingEgine(unitofWork, mockHub);
    }

    public static PasswordVerifyer GetPasswordVerifyer()
    {
        string[] passwordPatterns = { @"[0-9]+", @"[A-Z]+", @"[a-z]+", @".{8,}" };
        PasswordOptions options = new PasswordOptions(passwordPatterns);
        IOptions<PasswordOptions> pwOptions = Options.Create(options);

        PasswordVerifyer passwordVerifyer = new PasswordVerifyer(pwOptions);
        return passwordVerifyer;

    }

    public static async Task<AuthenticationService> GetAuthenticationServiceAsync()
    {
        PasswordVerifyer pwVerifyer = GetPasswordVerifyer();
        UnitofWork unitofWork = await GetUnitofWork();
        return new AuthenticationService(pwVerifyer, unitofWork);
    }

}
