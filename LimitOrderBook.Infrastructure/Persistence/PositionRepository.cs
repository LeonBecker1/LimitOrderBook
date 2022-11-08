using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LimitOrderBook.Application.Persistence;
using LimitOrderBook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using LimitOrderBook.Application.Exceptions;
using LimitOrderBook.Infrastructure.Persistence.Models;

namespace LimitOrderBook.Infrastructure.Persistence;

public class PositionRepository : Repository<Position>, IPositionRepository
{
    private readonly DbContext _context = null!;

    private readonly IMapper _mapper = null!;

    public PositionRepository(DbContext context, IMapper mapper) : base(context, mapper)
    {
        _context = context;
        _mapper  = mapper; 
    }

    public async Task<Position> AddPositionAsync(int StockId, int PortfolioId, uint Quantity)
    {
        StockModel? stockModel = await _context.Set<StockModel>().FindAsync(StockId);
        PortfolioModel? portfolioModel = await _context.Set<PortfolioModel>().FindAsync(PortfolioId);

        if(stockModel is null ||portfolioModel is null)
        {
            throw new QueryException("error occured");
        }
        else
        {
            PositionModel positionModel = new PositionModel(0, stockModel, Quantity);
            if(portfolioModel.positions is null)
            {
                portfolioModel.positions = new List<PositionModel>();
            }

            _context.Set<PositionModel>().Add(positionModel);
            portfolioModel.positions.Add(positionModel);
            await _context.SaveChangesAsync();
            return _mapper.Map<Position>(positionModel);    
        }
    }

    public async Task<Position> AddPositionAsync(Position Position, int PortfolioId)
    {
        PortfolioModel? portfolioModel = await _context.Set<PortfolioModel>().FindAsync(PortfolioId);
        if (portfolioModel is null)
        {
            throw new QueryException("Portfolio with Id " + PortfolioId + " does not exist");
        }
        else
        {
            PositionModel positionModel = _mapper.Map<PositionModel>(Position);
            if(portfolioModel.positions is null){
                portfolioModel.positions = new List<PositionModel>();
            }

            _context.Set<PositionModel>().Add(positionModel);
            await _context.SaveChangesAsync();

            PositionModel? positionModel1 = await _context.Set<PositionModel>().FindAsync(
                                   await _context.Set<PositionModel>().MaxAsync(p => p.positionId));

            portfolioModel.positions.Add(positionModel1!);
            await _context.SaveChangesAsync();
            return Position;
        }
    }

    public async Task<Position> DecrementPositionAsync(int PositionId, uint QtyDelta)
    {
        PositionModel positionModel = _context.Set<PositionModel>().FirstOrDefault(pm => pm.positionId == PositionId)!;
        
        if(positionModel is not null)
        {
            positionModel.quantity -= QtyDelta;
            await _context.SaveChangesAsync();
            return _mapper.Map<Position>(positionModel);
        }
        else
        {
            throw new QueryException("Position with Id " + PositionId.ToString() + " not found");
        }
    }

    public async Task<Position> DeletePosistionAsync(int PositionId)
    {
        PositionModel? positionModel = _context.Set<PositionModel>().Find(PositionId);
        if(positionModel is not null)
        {
            _context.Set<PositionModel>().Remove(positionModel);
            await _context.SaveChangesAsync();
            return _mapper.Map<Position>(positionModel);
        }
        else
        {
            throw new QueryException("Position with Id " + PositionId.ToString() + " does not exist in database");
        }
    }

    public async Task<List<Position>> GetAllPositionsByUserAsync(int UserId)
    {
        UserModel? userModel =  await _context.Set<UserModel>().FindAsync(UserId);

        if(userModel is not null)
        {
            List<PositionModel> positionModels = userModel.portfolio.positions;
            List<Position> positions = new List<Position>();
            foreach(PositionModel positionModel in positionModels)
            {
                Position position = _mapper.Map<Position>(positionModel);
                positions.Add(position);
            }
            return positions;
        }
        else
        {
            throw new QueryException("User with the Id" + UserId.ToString() + " does not exist");
        }
    }

    public async Task<Position> IncrementPositionAsync(int PositionId, uint QtyDelta)
    {
        PositionModel positionModel = _context.Set<PositionModel>().FirstOrDefault(pm => pm.positionId == PositionId)!;

        if (positionModel is not null)
        {
            positionModel.quantity *= QtyDelta;
            await _context.SaveChangesAsync();
            return _mapper.Map<Position>(positionModel);
        }
        else
        {
            throw new QueryException("Position with Id " + PositionId.ToString() + " not found");
        }
    }

    public async Task<Position> FindPositionAsync(int PositionId)
    {
        PositionModel? positionModel = await _context.Set<PositionModel>().FindAsync(PositionId);

        if(positionModel is not null)
        {
            return _mapper.Map<Position>(positionModel);
        }
        else
        {
            throw new QueryException("Position with id " + PositionId.ToString() + " does not work");
        }
    }
}
