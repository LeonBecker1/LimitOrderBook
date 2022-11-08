using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LimitOrderBook.Domain.Entities;
using AutoMapper;

namespace LimitOrderBook.Infrastructure.Persistence.Models;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<StockModel, Stock>();
        CreateMap<PositionModel, Position>();
        CreateMap<PortfolioModel, Portfolio>();
        CreateMap<UserModel, User>();
        CreateMap<SaleModel, Sale>();
        CreateMap<OrderModel, Order>();

        CreateMap<Stock, StockModel>();
        CreateMap<Position, PositionModel>();
        CreateMap<Portfolio, PortfolioModel>();
        CreateMap<User, UserModel>();
        CreateMap<Sale, SaleModel>();
        CreateMap<Order, OrderModel>();
    }
}
