using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using LimitOrderBook.Infrastructure.Persistence;
using LimitOrderBook.Infrastructure.Hubs;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Builder;
using LimitOrderBook.Application.Persistence;
using LimitOrderBook.Application.Services.Matching;
using LimitOrderBook.Infrastructure.Services.Matching;
using Microsoft.AspNetCore.Components;

namespace LimitOrderBook.Infrastructure;

public static  class DependencyInjector
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddResponseCompression(opts =>
        {
            opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                new[] { "application/octet-stream" });
        });
        services.AddDbContext<LimitOrderBookDbContext>(options => options.UseSqlServer(@"Data Source=INB220701\SQLSERVERLEON; Initial Catalog=LimitOrderBookDB;Integrated Security=True;"));
        services.AddAutoMapper(typeof(Program).Assembly);
        services.AddScoped<DbContext, LimitOrderBookDbContext>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPortfolioRepository, PortfolioRepository>();
        services.AddScoped<ISaleRepository, SaleRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IStockRepository, StockRepository>();
        services.AddScoped<IUnitofWork, UnitofWork>();
        services.AddScoped<IHubConnectionWrapper, TradingHubConnectionWrapper>();
        services.AddScoped<IMatchingEngine, MatchingEgine>();
        //services.AddScoped<NavigationManager>();
        //services.AddScoped<IMatchingService, MatchingService>();
        services.AddHostedService<MatchingService>();
        return services;
    }
}
