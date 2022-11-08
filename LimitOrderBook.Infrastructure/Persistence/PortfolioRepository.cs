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

public class PortfolioRepository : Repository<Portfolio>, IPortfolioRepository
{
    private readonly DbContext _context = null!;

    private readonly  IMapper _mapper = null!;

    public PortfolioRepository(DbContext context, IMapper mapper) : base(context, mapper)
    {
        _context = context;
        _mapper  = mapper;
    }

    

    public async Task<Position> AddPosistionAsync(Position Position, int PortfolioId)
    {
        PortfolioModel? portfolioModel = await _context.Set<PortfolioModel>().FirstOrDefaultAsync(p => p.portfolioId == PortfolioId);
        if (portfolioModel is null)
        {
            throw new QueryException("Portfolio with Id " + PortfolioId + " does not exist");
        }
        else
        {
            PositionModel positionModel = _mapper.Map<PositionModel>(Position);
            portfolioModel.positions.Add(positionModel);
            _context.Set<PositionModel>().Add(positionModel);
            await _context.SaveChangesAsync();
            return Position;
        }
    }

    public async Task<Portfolio> FindPortfolioAsync(int PortfolioId)
    {
        PortfolioModel? portfolioModel = await _context.Set<PortfolioModel>().FindAsync(PortfolioId);

        if(portfolioModel is not  null)
        {
            return _mapper.Map<Portfolio>(portfolioModel);
        }
        else
        {
            throw new QueryException("Portfolio with id " + PortfolioId.ToString() + " does not exist in database");
        }
    }

    
}
