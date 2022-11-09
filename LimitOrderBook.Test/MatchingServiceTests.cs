using LimitOrderBook.Application.Exceptions;
using LimitOrderBook.Domain.Entities;
using LimitOrderBook.Infrastructure.Persistence;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace LimitOrderBook.Test;

public class MatchingServiceTests
{

    [Fact]
    public async void Test()
    {
        var unitofWork = await UtilityClass.GetUnitofWork();

        Stock stock = new Stock("AAPL");
        Portfolio portfolio = new Portfolio(new List<Position>());
        User user = new User("Leon", "123456", 100, portfolio);
        await unitofWork.Stocks.AddStockAsync(stock);
        await unitofWork.Users.AddUserAsync(user);
        User user1 = await unitofWork.Users.FindUserAsync(1);
        Stock stock1 = await unitofWork.Stocks.FindByAbbreviationAsync("AAPL");
        Order buyOrder = new Order(10, 5, stock1, user1, true);
        Order sellOrder = new Order(10, 5, stock1, user1, false);
        await unitofWork.Orders.AddOrderAsync(buyOrder);
        await unitofWork.Orders.AddOrderAsync(sellOrder);
        List<Order> orders = await unitofWork.Orders.GetAllOrdersByIssuerAsync(1);

        Assert.NotNull(user1);
        Assert.NotNull(stock1);
        Assert.NotNull(orders);

    }

    [Fact]
    public async void OrdersFromSameUserDontGetMatched() 
    {

        var MatchingEngine = await UtilityClass.GetMatchingEngine();
        var unitofWork = await UtilityClass.GetUnitofWork();

        Stock stock = new Stock("AAPL");
        Portfolio portfolio = new Portfolio(new List<Position>());
        User user = new User("Leon", "123456", 100, portfolio);
        await unitofWork.Stocks.AddStockAsync(stock);
        await unitofWork.Users.AddUserAsync(user);
        User user1 = await unitofWork.Users.FindUserAsync(1);
        Stock stock1 = await unitofWork.Stocks.FindByAbbreviationAsync("AAPL");

        Order buyOrder = new Order(10, 5, stock1, user1, true);
        Order sellOrder = new Order(10, 5, stock1, user1, false);

        await unitofWork.Orders.AddOrderAsync(buyOrder);
        await unitofWork.Orders.AddOrderAsync(sellOrder);

        await MatchingEngine.MatchOrders();
        Assert.NotNull(await unitofWork.Orders.GetAllOrdersByIssuerAsync(1));
    }

    [Fact]
    public async void OrdersWithSpreadDontGetMatched()
    {
        var MatchingEngine = await UtilityClass.GetMatchingEngine();
        var unitofWork     = await UtilityClass.GetUnitofWork();

        Stock stock = new Stock("AAPL");
        Portfolio portfolio = new Portfolio(new List<Position>());
        User user = new User("Leon", "123456", 100, portfolio);
        Portfolio portfolio1 = new Portfolio(new List<Position>());
        User user1 = new User("Bill", "654321", 200, portfolio1);
        await unitofWork.Stocks.AddStockAsync(stock);
        await unitofWork.Users.AddUserAsync(user);
        await unitofWork.Users.AddUserAsync(user1);
        User user2 = await unitofWork.Users.FindUserAsync(1);
        User user3 = await unitofWork.Users.FindUserAsync(2);
        Stock stock1 = await unitofWork.Stocks.FindByAbbreviationAsync("AAPL");

        Order buyOrder = new Order(10, 5, stock1, user2, true);
        Order sellOrder = new Order(20, 5, stock1 , user3, false);
        await unitofWork.Orders.AddOrderAsync(buyOrder);
        await unitofWork.Orders.AddOrderAsync(sellOrder);

        await MatchingEngine.MatchOrders();
        Assert.NotNull(await unitofWork.Orders.GetAllOrdersByIssuerAsync(1));
        Assert.NotNull(await unitofWork.Orders.GetAllOrdersByIssuerAsync(2));

    }

    [Fact]
    public async void OrdersForDifferentUnderlyingDontGetMatched()
    {
        var MatchingEngine = await UtilityClass.GetMatchingEngine();
        var unitofWork = await UtilityClass.GetUnitofWork();

        Stock stock = new Stock("AAPL");
        Stock stock1 = new Stock("MSFT");
        Portfolio portfolio = new Portfolio(new List<Position>());
        User user = new User("Leon", "123456", 100, portfolio);
        Portfolio portfolio1 = new Portfolio(new List<Position>());
        User user1 = new User("Bill", "654321", 200, portfolio1);
        await unitofWork.Stocks.AddStockAsync(stock);
        await unitofWork.Stocks.AddStockAsync(stock1);
        await unitofWork.Users.AddUserAsync(user);
        await unitofWork.Users.AddUserAsync(user1);
        User user2 = await unitofWork.Users.FindUserAsync(1);
        User user3 = await unitofWork.Users.FindUserAsync(2);
        Stock stock2 = await unitofWork.Stocks.FindByAbbreviationAsync("AAPL");
        Stock stock3 = await unitofWork.Stocks.FindByAbbreviationAsync("MSFT");

        Order buyOrder = new Order(10, 5, stock2, user2, true);
        Order sellOrder = new Order(10, 5, stock3, user3, false);

        await unitofWork.Orders.AddOrderAsync(buyOrder);
        await unitofWork.Orders.AddOrderAsync(sellOrder);

        await MatchingEngine.MatchOrders();
        Assert.NotNull(await unitofWork.Orders.GetAllOrdersByIssuerAsync(1));
        Assert.NotNull(await unitofWork.Orders.GetAllOrdersByIssuerAsync(2));
    }

    [Fact]
    public async void OrdersWithoutSpreadGetMatched()
    {
        var MatchingEngine = await UtilityClass.GetMatchingEngine();
        var unitofWork = await UtilityClass.GetUnitofWork();

        Stock stock = new Stock("AAPL");
        Portfolio portfolio = new Portfolio(new List<Position>());
        User user = new User("Leon", "123456", 100, portfolio);
        Portfolio portfolio1 = new Portfolio(new List<Position>());
        User user1 = new User("Bill", "654321", 200, portfolio1);
        await unitofWork.Stocks.AddStockAsync(stock);
        await unitofWork.Users.AddUserAsync(user);
        await unitofWork.Users.AddUserAsync(user1);
        User user2 = await unitofWork.Users.FindUserAsync(1);
        User user3 = await unitofWork.Users.FindUserAsync(2);
        Stock stock1 = await unitofWork.Stocks.FindByAbbreviationAsync("AAPL");

        Order buyOrder = new Order(10, 5, stock1, user2, true);
        Order sellOrder = new Order(10, 5, stock1, user3, false);

        await unitofWork.Orders.AddOrderAsync(buyOrder);
        await unitofWork.Orders.AddOrderAsync(sellOrder);

        await MatchingEngine.MatchOrders();
        List<Order> orders = await unitofWork.Orders.GetAllOrdersByIssuerAsync(1);
        List<Order> orders1 = await unitofWork.Orders.GetAllOrdersByIssuerAsync(2);
        Assert.Equal(0, orders.Count());
        Assert.Equal(0, orders1.Count());

    }

    [Fact]
    public async void OrderWithBiggerQuantityRemains()
    {

    }

    [Fact]
    public async void OrderExecutionAffectIssuersBalanceAndPortfolio()
    {

    }
}
