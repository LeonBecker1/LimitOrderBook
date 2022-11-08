using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LimitOrderBook.Application.Persistence;
using LimitOrderBook.Domain.Entities;
using LimitOrderBook.Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using LimitOrderBook.Application.Exceptions;

namespace LimitOrderBook.Infrastructure.Persistence;

public class StockRepository : Repository<Stock>, IStockRepository
{
    private readonly DbContext _context = null!;

    private readonly IMapper _mapper = null!;

    public StockRepository(DbContext context, IMapper mapper) : base(context, mapper)
    {
        _context = context;
        _mapper  = mapper;
    }

    public async Task<Stock> AddStockAsync(Stock stock)
    {
        _context.Set<StockModel>().Add(_mapper.Map<StockModel>(stock));
        await _context.SaveChangesAsync();
        return stock;
    }

    public async Task<Stock> DeleteStockAsync(int StockId)
    {
        StockModel? stockModel = await _context.Set<StockModel>().FindAsync(StockId);

        if(stockModel is not null)
        {
            _context.Set<StockModel>().Remove(stockModel);
            await _context.SaveChangesAsync();
            return _mapper.Map<Stock>(stockModel);
        }
        else
        {
            throw new QueryException("Stock with id" + StockId.ToString() + " does not exist in database");
        }
    }

    public async Task<Stock> FindByAbbreviationAsync(string Abbreviation)
    {
        StockModel? stockModel = await _context.Set<StockModel>().FirstOrDefaultAsync(stock => stock.abbreviation == Abbreviation);

        if(stockModel is not null)
        {
            return _mapper.Map<Stock>(stockModel);
        }
        else
        {
            throw new QueryException("Stock named " + Abbreviation + " does not exist in database");
        }
    }

    public async Task<Stock> FindStockAsync(int StockId)
    {
        Stock stock = _mapper.Map<Stock>(await _context.Set<StockModel>().FindAsync(StockId));

        if(stock is not null)
        {
            return stock;
        }

        else
        {
            throw new QueryException("Stock with id" + StockId.ToString() + " does not exist in database");
        }
    }

    public async Task<List<Stock>> GetAllStocksAsync()
    {
        List<StockModel> stocks = await _context.Set<StockModel>().ToListAsync();
        List<Stock> result = new List<Stock>();
        foreach(StockModel stockModel in stocks)
        {
            Stock stock = _mapper.Map<Stock>(stockModel);
            result.Add(stock);
        }

        return result;
    }
}
