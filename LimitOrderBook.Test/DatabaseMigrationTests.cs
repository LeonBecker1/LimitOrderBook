using LimitOrderBook.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LimitOrderBook.Infrastructure.Persistence.Models;
using LimitOrderBook.Application.Exceptions;
using LimitOrderBook.Domain.Entities;
using AutoMapper;
using Xunit;
namespace LimitOrderBook.Test;

public class DatabaseMigrationTests
{


    [Fact]
    public async void RetreivingOrdersByStockWorks()
    {
        OrderRepository orderRepository = await UtilityClass.GetPersistentOrderRepository();
        List<Order> orders = await orderRepository.GetAllOrdersByStockAsync(43);
        Assert.NotNull(orders[0]);
    }

    [Fact]
    public async void Setup2()
    {
       UserRepository userRepository = await UtilityClass.GetPersistentUserRepository();
       User user = await userRepository.FindUserAsync(23);
       Assert.NotNull(user.portfolio);
    }


    [Fact]
    public async void Setup3()
    {
       PortfolioRepository portfolioRepository = await UtilityClass.GetPersistentPortfolioRepository();
        Portfolio portfolio = await portfolioRepository.FindPortfolioAsync(25);
        Assert.NotNull(portfolio.positions[0]);
    }

    [Fact]
    public async void Setup()
    {
        UserRepository userRepository = await UtilityClass.GetPersistentUserRepository();
        StockRepository stockRepository = await UtilityClass.GetPersistentStockRepository();
        PortfolioRepository portfolioRepository = await UtilityClass.GetPersistentPortfolioRepository();
        PositionRepository positionRepository = await UtilityClass.GetPersistentPositionRepository();
        OrderRepository orderRepository = await UtilityClass.GetPersistentOrderRepository();

        Stock stock = new Stock("Gold");
        Stock stock2 = new Stock("Silver");

        Portfolio portfolio = new Portfolio(new List<Position>());
        User user = new User("Rick", "helloworld", 100, portfolio);

        
        await userRepository.AddUserAsync(user);
        await stockRepository.AddStockAsync(stock);
        await stockRepository.AddStockAsync(stock2); 

        await positionRepository.AddPositionAsync(59, 25, 20);
        await positionRepository.AddPositionAsync(60, 25, 15);

        Stock stock3 = await stockRepository.FindStockAsync(57);
        Stock stock4 = await stockRepository.FindStockAsync(58);
        User user2 = await userRepository.FindUserAsync(24);

        Order order = new Order(15, 5, stock3, user2, false);
        Order order2 = new Order(4, 3, stock4, user2, true);

        await orderRepository.AddOrderAsync(order);
        await orderRepository.AddOrderAsync(order2);


    }

    [Fact]
    public async void Tesst()
    {
        Stock stock = new Stock("fuck");
        StockRepository stockRepository = await UtilityClass.GetPersistentStockRepository();
        await stockRepository.AddStockAsync(stock);
        Position position = new Position(stock, 5);
        Assert.NotNull(position);
        PositionRepository positionRepository = await UtilityClass.GetPersistentPositionRepository();
        await positionRepository.AddPositionAsync(position, 17);
        PortfolioRepository  pr = await UtilityClass.GetPersistentPortfolioRepository();
        UserRepository ur = await UtilityClass.GetPersistentUserRepository();
        Portfolio p = await pr.FindPortfolioAsync(17);
        User u = await ur.FindUserAsync(17);
        Assert.NotNull(u.portfolio.positions[0]);
    }

    [Fact]
    public async void Tessst()
    {
        //Portfolio p = new Portfolio(new List<Position>());
        //User user = new User("Max", "hello", 1000, p);
        //UserRepository ur = await UtilityClass.GetPersistentUserRepository();
        //await ur.AddUserAsync(user);   
        
        StockRepository stockRepository = await UtilityClass.GetPersistentStockRepository();
        //Stock stock = new Stock("dduck");
        //await stockRepository.AddStockAsync(stock);
        Stock stock1 = await stockRepository.FindStockAsync(36);
        /* Position position = new Position(stock1, 5);
        PositionRepository pr = await UtilityClass.GetPersistentPositionRepository();
        await pr.AddPositionAsync(position, 24); */

        PositionRepository pr = await UtilityClass.GetPersistentPositionRepository();
        await pr.AddPositionAsync(36, 24, 10);
    }

    [Fact]
    public async void PersistentStoringWorks()
    {
        // We can delete and add elements to a Sql-Server Database. (not in that order)

        StockRepository stockRepository = await UtilityClass.GetPersistentStockRepository();
        Stock stock = new Stock("AAPL");
        await stockRepository.AddStockAsync(stock);
        Stock stock1 = await stockRepository.FindStockAsync(5);
        Assert.NotNull(stock1);

        await stockRepository.DeleteStockAsync(5);
        await Assert.ThrowsAsync<QueryException>(() => stockRepository.FindStockAsync(5));
    }

    [Fact]
    public async void SalesDontCascade()
    {
        // deleting user or stock doesnt delete sale
        StockRepository stockRepository = await UtilityClass.GetPersistentStockRepository();
        UserRepository userRepository = await UtilityClass.GetPersistentUserRepository();
        SaleRepository saleRepository = await UtilityClass.GetPersistentSaleRepository();

        Stock stock = new Stock("AAPL");
        Portfolio portfolio1 = new Portfolio(new List<Position>());
        Portfolio portfolio2 = new Portfolio(new List<Position>());
        User user1 = new User("Leon", "123456", 10, portfolio1);
        User user2 = new User("Charlie", "654321", 20, portfolio2);
        

        await stockRepository.AddStockAsync(stock);
        await userRepository.AddUserAsync(user1);
        await userRepository.AddUserAsync(user2);

        Stock stock1 = await stockRepository.FindStockAsync(27);
        User user3 = await userRepository.FindUserAsync(12);
        User user4 = await userRepository.FindUserAsync(13);

        Sale sale1 = new Sale(stock1, user3, user4);
        Sale sale2 = new Sale(stock1, user3, user4);

        Assert.NotNull(stock1);
        Assert.NotNull(user3);
        Assert.NotNull(user4);

        await saleRepository.AddSaleAsync(sale1);
        await saleRepository.AddSaleAsync(sale2);
        Sale sale3 = await saleRepository.FindSaleAsync(3);
        Sale sale4 = await saleRepository.FindSaleAsync(4);
        Assert.NotNull(sale3);
        Assert.NotNull(sale4);

        await userRepository.DeleteUserAsync(12);
        await Assert.ThrowsAsync<QueryException>(() => saleRepository.FindSaleAsync(3));
        await Assert.ThrowsAsync<QueryException>(() => saleRepository.FindSaleAsync(4));


        User user31 = new User("Leon", "123456", 10, portfolio1);
        await userRepository.AddUserAsync(user31);
        User user5 = await userRepository.FindUserAsync(14);
        sale1.buyer = user4; sale1.seller = user5;
        sale2.buyer = user4; sale2.seller = user5;
        await saleRepository.AddSaleAsync(sale1);
        await saleRepository.AddSaleAsync(sale2);

        await stockRepository.DeleteStockAsync(27);
        await Assert.ThrowsAsync<QueryException>(() => saleRepository.FindSaleAsync(5));
        await Assert.ThrowsAsync<QueryException>(() => saleRepository.FindSaleAsync(6));
    }

    [Fact]
    public async void PortfoliosCascadse()
    {
        // deleting users delets portfolio
        PortfolioRepository portfolioRepository = await UtilityClass.GetPersistentPortfolioRepository();
        UserRepository userRepository = await UtilityClass.GetPersistentUserRepository();

        Portfolio portfolio = new Portfolio(new List<Position>());
        User user = new User("Leon", "123456", 10, portfolio);

        await userRepository.AddUserAsync(user);

        User user1 = await userRepository.FindUserAsync(5);
        Portfolio portfolio1 = await portfolioRepository.FindPortfolioAsync(4);

        Assert.NotNull(portfolio1);
        Assert.NotNull(user1);

        await userRepository.DeleteUserAsync(5);

        await Assert.ThrowsAsync<QueryException>(() => userRepository.FindUserAsync(5));
        await Assert.ThrowsAsync<QueryException>(() => portfolioRepository.FindPortfolioAsync(4));

    }

    [Fact]
    public async void StocksDontCascade()
    {
        // deleting postion doesnt delete stock
        StockRepository stockRepository = await UtilityClass.GetPersistentStockRepository();
        PositionRepository positionRepository = await UtilityClass.GetPersistentPositionRepository();
        PortfolioRepository portfolioRepository = await UtilityClass.GetPersistentPortfolioRepository();
        UserRepository userRepository = await UtilityClass.GetPersistentUserRepository();

        Stock stock = new Stock("AAPL");
        Position position = new Position(stock, 21);
        List<Position> positions = new List<Position>();
        Portfolio portfolio = new Portfolio(positions);
        User user = new User("Leon", "123456", 10, portfolio);

        await stockRepository.AddStockAsync(stock);
        await userRepository.AddUserAsync(user);
        await positionRepository.AddPositionAsync(position, 3);


        Stock stock1 = await stockRepository.FindStockAsync(21);
        Position position1 = await positionRepository.FindPositionAsync(3);
        User user1 = await userRepository.FindUserAsync(4);

        Assert.NotNull(stock1);
        Assert.NotNull(position1);
        Assert.NotNull(user1);

        await positionRepository.DeletePosistionAsync(3);
        Stock stock2 = await stockRepository.FindStockAsync(21);
        Assert.NotNull(stock2);

        await stockRepository.DeleteStockAsync(21);
        await userRepository.DeleteUserAsync(4);
    }

    [Fact]
    public async void PositionsCascade()
    {
        // deleting user deletes position 
        PositionRepository positionRepository = await UtilityClass.GetPersistentPositionRepository();
        PortfolioRepository portfolioRepository = await UtilityClass.GetPersistentPortfolioRepository();
        StockRepository stockRepository = await UtilityClass.GetPersistentStockRepository();
        UserRepository userRepository = await UtilityClass.GetPersistentUserRepository();

        Stock stock = new Stock("AAPL");
        Position position = new Position(stock, 10);
        Portfolio portfolio = new Portfolio(new List<Position>());
        User user = new User("Leon", "123456", 10, portfolio);

        await stockRepository.AddStockAsync(stock);
        await userRepository.AddUserAsync(user);
        User user1 = await userRepository.FindUserAsync(20);
        Stock stock1 = await stockRepository.FindStockAsync(34);
        Portfolio portfolio1 = await portfolioRepository.FindPortfolioAsync(20);
        Assert.NotNull(user1);
        Assert.NotNull(stock1);
        Assert.NotNull(portfolio1);

        await positionRepository.AddPositionAsync(position, 20);
        Position position1 = await positionRepository.FindPositionAsync(6);
        Assert.NotNull(position1);

        await userRepository.DeleteUserAsync(20);
        await Assert.ThrowsAnyAsync<QueryException>(() => positionRepository.FindPositionAsync(6));

    }

    [Fact]
    public async void PositionsDontCascade()
    {
        // deleting stock does not delete positon
    }

    [Fact]
    public async void OrdersCascade()
    {
        // deleting user and stock deletes order
    }
}
