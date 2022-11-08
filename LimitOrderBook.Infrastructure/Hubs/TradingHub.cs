using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LimitOrderBook.Domain.Entities;
using Microsoft.AspNetCore.SignalR;


namespace LimitOrderBook.Infrastructure.Hubs;

public class TradingHub : Hub
{
    public Task SendOrderAddition(Order order)
    {
        return Clients.All.SendAsync("ReceiveOrderAddition", order);
    }

    public Task SendOrderDeletion(int orderId)
    {
        return Clients.All.SendAsync("ReceiveOrderDeletion", orderId);
    }

    public Task SendOrderMatching(int buyOrderId, int sellOrderId,
                                  uint buyOrderQuantity, uint sellOrderQuantity)
    {
        return Clients.All.SendAsync("ReceiveOrderMatching", buyOrderId, sellOrderId,
                                     buyOrderQuantity, sellOrderQuantity);
    }

    public Task SendTest(String test) 
    {
        return Clients.All.SendAsync("ReceiveTest", test);
    }

    public Task SendTest2(Stock stock)
    {
        return Clients.All.SendAsync("ReceiveTest2", stock);
    }

}
