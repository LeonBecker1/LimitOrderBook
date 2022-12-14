using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LimitOrderBook.Domain.Entities;
using Microsoft.AspNetCore.SignalR;

namespace LimitOrderBook.Infrastructure.Hubs;

public class ChatHub : Hub
{

    public Task SendMessage(string user, string message)
    {
        return Clients.All.SendAsync("ReceiveMessage", user, message);
    }

    public Task SendStock(Stock stock)
    {
        return Clients.All.SendAsync("ReceiveStock", stock);
    }

}
