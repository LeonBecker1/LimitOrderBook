using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LimitOrderBook.Infrastructure.Persistence.Models;
using LimitOrderBook.Domain.Entities;
using AutoMapper;
using LimitOrderBook.Infrastructure.Persistence;

namespace LimitOrderBook.Test;

public class MappingTests
{
    
    private IMapper GetMapper()
    {
        var mockMapper = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new AutoMapperProfile());
        });
        return mockMapper.CreateMapper();
    }

    private List<PositionModel> GetSamplePositionModels()
    {
        List<PositionModel> positionModels = new();
        for (int i = 0; i <= 10; i++)
        {
            StockModel stockModel = new StockModel(i, i.ToString());
            PositionModel positionModel = new PositionModel(i, stockModel, (uint)i);
            positionModels.Add(positionModel);
        }

        return positionModels;
    }

    private UserModel GetSampleUser()
    {
        List<PositionModel> positionModels = GetSamplePositionModels();
        PortfolioModel portfolioModel = new PortfolioModel(1, positionModels);
        UserModel userModel = new UserModel(1, "Leon", "123456", 10, portfolioModel);
        return userModel;
    }

    private bool ComparePositions(PositionModel positionModel, Position position)
    {
        return position.quantity == positionModel.quantity && position.underlying.abbreviation == positionModel.underlying.abbreviation;
    }

    private bool ComparePortfolios(PortfolioModel portfolioModel, Portfolio portfolio)
    {
        bool result = true;
        List<PositionModel> positionModels = portfolioModel.positions;
        List<Position> positions = portfolio.positions;

        if (positions.Count != positionModels.Count)
        {
            return false;
        }
        else
        {
            for (int i = 0; i < positions.Count; i++)
            {
                result = result && ComparePositions(positionModels[i], positions[i]);
            }

            return result;
        }
    }

    private bool CompareUsers(UserModel userModel, User user)
    {
        return ComparePortfolios(userModel.portfolio, user.portfolio) && user.balance == userModel.balance && user.userName == userModel.userName && user.password == userModel.password;
    }

    private bool CompareOrders(OrderModel orderModel, Order order)
    {
       return CompareUsers(orderModel.issuer, order.issuer) && orderModel.underlying.abbreviation == order.underlying.abbreviation && orderModel.price == order.price && orderModel.quantity == order.quantity;
    }

    [Fact]
    public void StockModelMappingWorks()
    {
        IMapper mapper = GetMapper();
        StockModel stockModel = new StockModel(1, "AAPL");
        Stock stock = mapper.Map<Stock>(stockModel);
        Assert.Equal("AAPL", stock.abbreviation);
    }

    [Fact]
    public void PositionModelMappingWorks()
    {
        IMapper mapper = GetMapper();
        StockModel stockModel = new StockModel(1, "AAPL");
        PositionModel positionModel = new PositionModel(1, stockModel, 10);
        Position position = mapper.Map<Position>(positionModel);
        bool testResult = ComparePositions(positionModel, position);
        Assert.True(testResult);
    }

    [Fact]
    public void PortfolioModelMappingWorks()
    {
        IMapper mapper = GetMapper();
        List<PositionModel> positionModels = GetSamplePositionModels();
        PortfolioModel portfolioModel = new PortfolioModel(1, positionModels);
        Portfolio portfolio = mapper.Map<Portfolio>(portfolioModel);

        bool testResult = ComparePortfolios(portfolioModel, portfolio); 
        Assert.True(testResult);
    }

    [Fact]
    public void UserModelMappingWorks() {
        IMapper mapper = GetMapper();
        UserModel userModel = GetSampleUser();
        User user = mapper.Map<User>(userModel);

        bool testResult = CompareUsers(userModel, user);
        Assert.True(testResult);
    }

    [Fact]
    public void SaleModelMappingWorks()
    {
        IMapper mapper = GetMapper();
        UserModel buyerModel = GetSampleUser();
        UserModel sellerModel = GetSampleUser();
        StockModel stockModel = new StockModel(1, "AAPL");
        SaleModel saleModel = new SaleModel(1, stockModel, buyerModel, sellerModel);
        Sale sale = mapper.Map<Sale>(saleModel);

        bool testResult = CompareUsers(saleModel.seller, sale.seller) && CompareUsers(saleModel.buyer, sale.buyer) && saleModel.underlying.abbreviation == sale.underlying.abbreviation;
        Assert.True(testResult);
    }

    [Fact]
    public void OrderModelMappingWorks()
    {
        IMapper mapper = GetMapper();
        UserModel userModel = GetSampleUser();
        StockModel stockModel = new StockModel(1, "AAPL");
        OrderModel orderModel = new OrderModel(1, 10, 100, stockModel, userModel, true);
        Order order = mapper.Map<Order>(orderModel);

        bool testResult = CompareOrders(orderModel, order);

        Assert.True(testResult);
    }
    
}
