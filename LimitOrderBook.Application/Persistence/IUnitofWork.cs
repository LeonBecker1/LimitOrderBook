using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimitOrderBook.Application.Persistence;

public interface IUnitofWork : IDisposable
{
    IUserRepository Users { get; }

    IStockRepository Stocks { get; }

    IPortfolioRepository Portfolios { get; }

    IOrderRepository Orders { get; }

    ISaleRepository Sales { get; }

    int Complete();

}
