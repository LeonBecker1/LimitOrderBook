using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LimitOrderBook.Application.Persistence;
using LimitOrderBook.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LimitOrderBook.Infrastructure.Persistence;

public class UnitofWork : IUnitofWork
{
    private readonly DbContext _context = null!;

    private readonly IMapper _mapper = null!;

    public UnitofWork(DbContext context, IMapper mapper)
    {
        _context = context;
        _mapper  = mapper; 

        Orders     = new OrderRepository(_context, _mapper);
        Users      = new UserRepository(_context, _mapper);
        Stocks     = new StockRepository(_context, _mapper);
        Sales      = new SaleRepository(_context, _mapper);
        Portfolios = new PortfolioRepository(_context, _mapper);
        Positions  = new PositionRepository(_context, _mapper);

    }

    public IOrderRepository Orders { get; private set; }

    public IUserRepository Users { get; private set; }

    public IStockRepository Stocks { get; private set; }

    public ISaleRepository Sales { get; private set; }

    public IPortfolioRepository Portfolios { get; private set; }

    public IPositionRepository Positions { get; private set;}

    public int Complete()
    {
        return _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
