using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LimitOrderBook.Infrastructure.Persistence;
using LimitOrderBook.Infrastructure.Persistence.Models;
using LimitOrderBook.Application.Exceptions;
using LimitOrderBook.Domain.Entities;
using AutoMapper;
using Xunit;

namespace LimitOrderBook.Test;

public class RepositoryTests
{



    [Fact]
    public async void InstantiationWorks()
    {
        // Using Automapper and getting DbContext / Repositories works
        IMapper mapper = UtilityClass.GetMapper();
        Assert.NotNull(mapper);

        LimitOrderBookDbContext dbContext = await UtilityClass.GetDbContext();
        Assert.NotNull(dbContext);

        UserRepository userRepository = await UtilityClass.GetUserRepository();
        Assert.NotNull(userRepository);
    }

    [Fact]
    public async void DatabaseKeyGenerationWorks()
    {
        // Database keygeneration-strategy works as intendet
        StockRepository stockRepository = await UtilityClass.GetStockRepository();
        for(int i = 0; i < 10; i++)
        {
            Stock stock = new Stock(i.ToString());
            await stockRepository.AddStockAsync(stock);
            Stock stock1 = await stockRepository.FindStockAsync(i + 1);
            Assert.NotNull(stock1);
        }
    }

    [Fact]
    public async void QueryExceptionWorks()
    {
        // Queries on entities, that don't exist throw custom exception
        PositionRepository positionRepository = await UtilityClass.GetPositionRepository();

        await Assert.ThrowsAsync<QueryException>(() => positionRepository.DeletePosistionAsync(20));
    }
    
    [Fact]
    public async void AddingUsersWorks()
    {
        // Adding Users Works
        // Finding Users Works
        // Finding Portfolios works
        UserRepository userRepository = await UtilityClass.GetUserRepository();
        PortfolioRepository portfolioRepository = await UtilityClass.GetPortfolioRepository();

        Portfolio portfolio = new Portfolio(new List<Position>());
        User user = new User("Leon", "123456", 100, portfolio);

        await userRepository.AddUserAsync(user);

        User user1 = await userRepository.FindUserAsync(1);
        Assert.NotNull(user1);

        Assert.NotNull(user1);
        Assert.NotNull(user1.portfolio);
    } 

    [Fact]
    public async void DeletingUsersWorks()
    {
        //Deleting users works.

        UserRepository userRepository = await UtilityClass.GetUserRepository();
        PortfolioRepository portfolioRepository = await UtilityClass.GetPortfolioRepository();

        Portfolio portfolio = new Portfolio(new List<Position>());
        User user = new User("Leon", "123456", 100, portfolio);

        await userRepository.AddUserAsync(user);
        User user1 = await userRepository.FindUserAsync(1);
        Assert.NotNull(user1);

        
        await userRepository.DeleteUserAsync(1);
        await Assert.ThrowsAnyAsync<QueryException>(() => userRepository.FindUserAsync(1)); 
        //await Assert.ThrowsAnyAsync<QueryException>(() => portfolioRepository.FindPortfolioAsync(1));

    }

    [Fact]
    public async void OrderOperationsWork()
    {
        // Adding Orders works
        // Modifying Orders works
        // Getting all Orders by Issuer or Stock works

        OrderRepository orderRepository = await UtilityClass.GetOrderRepository();
        UserRepository userRepository = await UtilityClass.GetUserRepository();
        StockRepository stockRepository = await UtilityClass.GetStockRepository();

        Stock stock = new Stock("AAPL");
        Portfolio portfolio = new Portfolio(new List<Position>());
        User user = new User("Leon", "123456", 10, portfolio);
        await stockRepository.AddStockAsync(stock);
        await userRepository.AddUserAsync(user);
        User user1 = await userRepository.FindUserAsync(1);
        Stock stock1 = await stockRepository.FindStockAsync(1);
        Order order = new Order(10, 100, stock1, user1, true);
        Order order1 = new Order(10, 100, stock1, user1, false);


        await orderRepository.AddOrderAsync(order);
        await orderRepository.AddOrderAsync(order1);

        Assert.NotNull(await orderRepository.FindOrderAsync(1));
        Assert.NotNull(await orderRepository.FindOrderAsync(2));
        Assert.Equal(2, orderRepository.GetAllOrdersByIssuerAsync(1).Result.Count);
        Assert.Equal(2, orderRepository.GetAllOrdersByStockAsync(1).Result.Count);

        await orderRepository.UpdateOrderAsync(1, 1000, 1000);
        Order order2 = await orderRepository.FindOrderAsync(1);

        Assert.Equal(1000, order2.price);



    }

    [Fact]
    public async void StockModelDeletionionWorks()
    {
        
    }

    [Fact]
    public async void PositionOperationsWork()
    {
        // adding, deleting, incrementing, decrementing, finding works
    }

    /*
    [Fact]
    public async void PositionModelInsertionWorks()
    {
        PortfolioRepository portfolioRepository = await UtilityClass.GetPortfolioRepository();
        PositionRepository positionRepository = await UtilityClass.GetPositionRepository();
        Portfolio portfolio = new Portfolio(new List<Position>());
        Stock stock = new Stock("AAPL");
        Position position = new Position(stock, 10);
        await portfolioRepository.AddPortfolio(portfolio);
        await portfolioRepository.AddPosistionAsync(position, 1);
        Position position1 = await positionRepository.FindAsync(1);
        Assert.NotNull(position1);
    }*/

    [Fact]
    public async void PositionModelDeletionWorks()
    {
        LimitOrderBookDbContext context = await UtilityClass.GetDbContext();
    }

    [Fact]
    public async void PortfolioModelDeletionWorks()
    {
        LimitOrderBookDbContext context = await UtilityClass.GetDbContext();
    }

    [Fact]
    public async void UserModelDeletionWorks()
    {
        LimitOrderBookDbContext context = await UtilityClass.GetDbContext();
    }

    [Fact]
    public async void SaleModelDeletionWorks()
    {
        LimitOrderBookDbContext context = await UtilityClass.GetDbContext();
    }

    [Fact]
    public async void OrderModelDeletionWorks()
    {
        LimitOrderBookDbContext context = await UtilityClass.GetDbContext();
    }

    [Fact]
    public async void StockModelInsertionWorks()
    {
        StockRepository stockRepository = await UtilityClass.GetStockRepository();
        Stock stock = new Stock("AAPL");

        await stockRepository.AddStockAsync(stock);
        Stock stock1 = await stockRepository.FindByAbbreviationAsync("AAPL");
        Assert.NotNull(stock1);
    }
 

    [Fact]
    public async void PortfolioModelInsertionWorks()
    {
        LimitOrderBookDbContext context = await UtilityClass.GetDbContext();
    }

    [Fact]
    public async void UserModelInsertionWorks()
    {
        LimitOrderBookDbContext context = await UtilityClass.GetDbContext();
    }

    [Fact]
    public async void SaleModelInsertionWorks()
    {
        LimitOrderBookDbContext context = await UtilityClass.GetDbContext();
    }

    [Fact]
    public async void OrderModelInsertionWorks()
    {
        LimitOrderBookDbContext context = await UtilityClass.GetDbContext();
    } 
}
