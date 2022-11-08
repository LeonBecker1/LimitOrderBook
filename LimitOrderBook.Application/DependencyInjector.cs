using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;


namespace LimitOrderBook.Application;

public static class DependencyInjector
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        //services.AddHostedService<MatchingService>();
        return services;
    }
}
