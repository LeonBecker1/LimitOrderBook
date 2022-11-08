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

public class SaleRepository : Repository<Sale>, ISaleRepository
{
    private readonly DbContext _context = null!;

    private readonly IMapper _mapper = null!;

    public SaleRepository(DbContext context, IMapper mapper) : base(context, mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Sale> AddSaleAsync(Sale sale)
    {
        SaleModel saleModel    = _mapper.Map<SaleModel>(sale);
        UserModel? seller      = await _context.Set<UserModel>().FindAsync(sale.buyer.userId);
        UserModel? buyer       = await _context.Set<UserModel>().FindAsync(sale.buyer.userId);
        StockModel? underlying = await _context.Set<StockModel>().FindAsync(sale.underlying.stockId);

        if(seller is null || buyer is null || underlying is null)
        {
            throw new QueryException("A member within the sale-object does not exist within the database");
        }
        else
        {
            saleModel.seller = seller;
            saleModel.buyer = buyer;
            saleModel.underlying = underlying;
            _context.Set<SaleModel>().Add(saleModel);
            await _context.SaveChangesAsync();
            return sale;
        }
    }

    public async Task<Sale> FindSaleAsync(int SaleId)
    {
        SaleModel? saleModel = await _context.Set<SaleModel>().FindAsync(SaleId);

        if (saleModel is not null)
        {
            return _mapper.Map<Sale>(saleModel);
        }
        else
        {
            throw new QueryException("Sale with Id " + SaleId.ToString() + " does not exist in databse");
        }
    }
}
