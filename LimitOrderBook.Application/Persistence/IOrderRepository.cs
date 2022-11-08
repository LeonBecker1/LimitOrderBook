using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LimitOrderBook.Domain.Entities;

namespace LimitOrderBook.Application.Persistence;

public interface IOrderRepository : IRepository<Order>
{

    Task<List<Order>> GetAllOrdersAsync();
    Task<List<Order>> GetAllOrdersByIssuerAsync(int IssuerId);

    Task<List<Order>> GetAllOrdersByStockAsync(int StockId);

    Task<Order> UpdateOrderAsync(int OrderId, int NewPrice, uint NewQty);

    Task<Order> AddOrderAsync(Order order);

    Task<Order> DeleteOrderAsync(int OrderId);
}
