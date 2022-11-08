using LimitOrderBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimitOrderBook.Application.Persistence;

public interface IPositionRepository : IRepository<Position>
{
    Task<List<Position>> GetAllPositionsByUserAsync(int UserId);

    Task<Position> DecrementPositionAsync(int PositionId, uint QtyDelta);

    Task<Position> IncrementPositionAsync(int PositionId, uint QtyDelta);

    Task<Position> DeletePosistionAsync(int PositionId);

    Task<Position> AddPositionAsync(Position Position, int PortfolioId);

    Task<Position> AddPositionAsync(int StockId, int PortfolioId, uint Quantity);



}
