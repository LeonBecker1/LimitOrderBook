using LimitOrderBook.Domain.Entities;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimitOrderBook.Infrastructure.Hubs;

public class TradingHubConnectionWrapper : IHubConnectionWrapper
{

    public TradingHubConnectionWrapper()
    {
        _hubConnection = new HubConnectionBuilder()
        .WithUrl("https://localhost:7186/TradingHub\r\n")
        .WithAutomaticReconnect()
        .Build();

        _hubConnection.StartAsync();
    }

    private readonly HubConnection _hubConnection;

    public async Task SendAsync(string methodName, params object[] parameters)
    {
        switch (methodName)
        {
            case "SendOrderAddition":
                await _hubConnection.SendAsync(methodName, (Order)parameters[0]);
                break;
            case "SendOrderDeletion":
                await _hubConnection.SendAsync(methodName, (int)parameters[0]);
                break;
            case "SendOrderMatching":
                await _hubConnection.SendAsync(methodName, (int)parameters[0], (int)parameters[1],
                                         (uint)parameters[2], (uint)parameters[3]);
                break;
            default:
                break;
        }
    }

}
