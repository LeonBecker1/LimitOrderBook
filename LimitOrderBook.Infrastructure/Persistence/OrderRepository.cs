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

public class OrderRepository : Repository<Order>, IOrderRepository
{
    private readonly DbContext _context = null!;

    private readonly IMapper _mapper = null!;

    public OrderRepository(DbContext context, IMapper mapper) : base(context, mapper)
    {
        _context = context;
        _mapper  = mapper;
    }

    public async Task<List<Order>> GetAllOrdersByIssuerAsync(int IssuerId)
    {
        List<OrderModel> orderModels = await _context.Set<OrderModel>().Where(orderModel => orderModel.issuer.userId == IssuerId).ToListAsync();
        List<Order> orders = new List<Order>();
        foreach(OrderModel orderModel in orderModels)
        {
            Order order = _mapper.Map<Order>(orderModel); 
            orders.Add(order);
        }
        return orders;
    }

    public async Task<List<Order>> GetAllOrdersAsync()
    {
        List<OrderModel> orderModels = await _context.Set<OrderModel>().ToListAsync();
        List<Order> orders = new List<Order>();
        foreach(OrderModel orderModel in orderModels)
        {
            orders.Add(_mapper.Map<Order>(orderModel));
        }
        return orders;
    }

    public async Task<List<Order>> GetAllOrdersByStockAsync(int StockId)
    {
        List<OrderModel> orderModels = await _context.Set<OrderModel>().Where(orderModel => orderModel.underlying.stockId == StockId).ToListAsync();
        List<Order> orders = new List<Order>();
        foreach (OrderModel orderModel in orderModels)
        {
            Order order = _mapper.Map<Order>(orderModel);
            orders.Add(order);
        }
        return orders;
    }

    public async Task<Order> UpdateOrderAsync(int OrderId, int NewPrice, uint NewQty)
    {
        OrderModel orderModel = _context.Set<OrderModel>().SingleOrDefault(orderModel => orderModel.orderId == OrderId)!;
        if(orderModel is not null)
        {
            orderModel.price = NewPrice;
            orderModel.quantity = NewQty;
            await _context.SaveChangesAsync();
            return _mapper.Map<Order>(orderModel);
        }
        else
        {
            throw new QueryException("There is no Order with Id " + OrderId.ToString() + " within the database");
        }

    }

    public async Task<Order> FindOrderAsync(int OrderId)
    {
        OrderModel? orderModel = await _context.Set<OrderModel>().FindAsync(OrderId);
        if(orderModel is not null)
        {
            return _mapper.Map<Order>(orderModel);
        }
        else
        {
            throw new QueryException("There is no Order with Id " + OrderId.ToString() + " within the database");
        }
    }

    public async Task<Order> AddOrderAsync(Order Order)
    {
         OrderModel orderModel = _mapper.Map<OrderModel>(Order);
         UserModel? userModel =  await _context.Set<UserModel>().FindAsync(Order.issuer.userId);
         StockModel? stockModel = await _context.Set<StockModel>().FindAsync(Order.underlying.stockId);

         orderModel.underlying = stockModel!;
         orderModel.issuer = userModel!;
         if(userModel is not null)
         {
             _context.Set<OrderModel>().Add(orderModel);
             await _context.SaveChangesAsync();
             OrderModel? orderModel2 = await _context.Set<OrderModel>().FirstOrDefaultAsync(o => o.orderId ==  _context.Set<OrderModel>().Max(o => o.orderId));
             Order.orderId = orderModel2!.orderId;
             return Order;
         }
         else
         {
             throw new QueryException("Issuer for given Order does not exist in Database");
         }
    }



    public async Task<Order> DeleteOrderAsync(int OrderId)
    {
        OrderModel? orderModel = _context.Set<OrderModel>().Find(OrderId);
        if(orderModel is not null)
        {
           _context.Set<OrderModel>().Remove(orderModel);
            await _context.SaveChangesAsync();
            return _mapper.Map<Order>(orderModel);
        }
        else
        {
            throw new QueryException("Order with Id " + OrderId.ToString() + " does not exist in database");
        }
    }
}
