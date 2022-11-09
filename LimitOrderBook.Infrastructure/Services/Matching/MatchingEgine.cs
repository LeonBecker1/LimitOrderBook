using LimitOrderBook.Application.Persistence;
using LimitOrderBook.Application.Services.Matching;
using LimitOrderBook.Domain.Entities;
using LimitOrderBook.Infrastructure.Hubs;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimitOrderBook.Infrastructure.Services.Matching;

public class MatchingEgine : IMatchingEngine
{

    public MatchingEgine(IUnitofWork unitofWork, IHubConnectionWrapper hubConnection)
    {
        _hubConnection = hubConnection;
        _unitofWork    = unitofWork;
    }

    private readonly IHubConnectionWrapper _hubConnection;

    private readonly IUnitofWork _unitofWork;

    public async Task MatchOrders()
    {
        Dictionary<string, (List<Order>, List<Order>)> ordersByStock = await FetchAndSortOrders();
        foreach (var ordersForStock in ordersByStock)
        {
            List<Order> buyOrders = ordersForStock.Value.Item1;
            List<Order> sellOrders = ordersForStock.Value.Item2;

            while (buyOrders.Count > 0 && sellOrders.Count > 0
                  && buyOrders[0].price >= sellOrders[0].price)
            {
                Order buyOrder = buyOrders[0];
                Order sellOrder = sellOrders[0];
                uint buyOrderQuantity = 0, sellOrderQuantity = 0;
                int sizeDifference = (int)buyOrder.quantity - (int)sellOrder.quantity;
                if (sizeDifference == 0)
                {
                    await _unitofWork.Orders.DeleteOrderAsync(buyOrder.orderId);
                    await _unitofWork.Orders.DeleteOrderAsync(sellOrder.orderId);
                    buyOrders.RemoveAt(0);
                    sellOrders.RemoveAt(0);
                }
                else if (sizeDifference > 0)
                {
                    buyOrderQuantity = (uint)sizeDifference;
                    await _unitofWork.Orders.UpdateOrderAsync(buyOrder.orderId, buyOrder.price, (uint)sizeDifference);
                    await _unitofWork.Orders.DeleteOrderAsync(sellOrder.orderId);
                    buyOrders[0].quantity = (uint)sizeDifference;
                    sellOrders.RemoveAt(0);
                }
                else if (sizeDifference < 0)
                {
                    sellOrderQuantity = (uint)(-1 * sizeDifference);
                    await _unitofWork.Orders.DeleteOrderAsync(buyOrder.orderId);
                    await _unitofWork.Orders.UpdateOrderAsync(sellOrder.orderId, sellOrder.price, (uint)(-1 * sizeDifference));
                    buyOrders.RemoveAt(0);
                    sellOrders[0].quantity = (uint)(-1 * sizeDifference);
                }

                await _hubConnection.SendAsync("SendOrderMatching", buyOrder.orderId,
                                               sellOrder.orderId, buyOrderQuantity,
                                               sellOrderQuantity);

            }

        }
    }

    private async Task<Dictionary<String, (List<Order>, List<Order>)>> FetchAndSortOrders()
    {
        List<Order> orders = await _unitofWork.Orders.GetAllOrdersAsync();
        Dictionary<String, (List<Order>, List<Order>)> ordersByStock = new();

        foreach (Order order in orders)
        {
            String abbreviation = order.underlying.abbreviation;
            if (!ordersByStock.ContainsKey(abbreviation))
            {
                ordersByStock.Add(abbreviation, (new List<Order>(), new List<Order>()));
            }

            if (order.isBuyOrder)
            {
                ordersByStock[abbreviation].Item1.Add(order);
            }
            else
            {
                ordersByStock[abbreviation].Item2.Add(order);
            }
        }
        foreach (var element in ordersByStock)
        {
            element.Value.Item1.Sort((o1, o2) => o1.price.CompareTo(o2.price));
            element.Value.Item2.Sort((o1, o2) => o2.price.CompareTo(o1.price));
        }

        return ordersByStock;
    }
}
