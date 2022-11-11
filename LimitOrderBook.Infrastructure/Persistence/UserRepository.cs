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

public class UserRepository : Repository<User>, IUserRepository
{
    private readonly DbContext _context = null!;

    private readonly  IMapper _mapper = null!;

    public UserRepository(DbContext context, IMapper mapper) : base(context, mapper)
    {
        _context = context;
        _mapper  = mapper;
    }

    public async Task<User> ChangeBalanceAsync(decimal BalanceDelta, int UserId)
    {
        UserModel? userModel = await _context.Set<UserModel>().FirstOrDefaultAsync(user => user.userId == UserId);

        if(userModel is not null)
        {
            userModel.balance += BalanceDelta;
            await _context.SaveChangesAsync();
            return _mapper.Map<User>(userModel);
        }
        else
        {
            throw new QueryException("User with Id " + UserId.ToString() + " does not exist");
        }
    }

    public async Task<User> AddUserAsync(User User)
    {
        PortfolioModel portfolioModel = _mapper.Map<PortfolioModel>(User.portfolio);
        _context.Set<PortfolioModel>().Add(portfolioModel);
        await _context.SaveChangesAsync();

        UserModel userModel = _mapper.Map<UserModel>(User);
        PortfolioModel? portfolioModel2 = await _context.Set<PortfolioModel>().FindAsync(
                                    await _context.Set<PortfolioModel>().MaxAsync(p => p.portfolioId));

        portfolioModel2!.positions = new List<PositionModel>();

        userModel.portfolio = portfolioModel2!;
        _context.Set<UserModel>().Add(userModel);
        await _context.SaveChangesAsync();

        return User;
    }

    public async Task<User> DeleteUserAsync(int UserId)
    {
        UserModel? userModel = await _context.Set<UserModel>().FindAsync(UserId);

        if(userModel is not null)
        {
            PortfolioModel? portfolioModel = await _context.Set<PortfolioModel>().FindAsync(userModel.portfolio.portfolioId);
            _context.Set<PortfolioModel>().Remove(portfolioModel!);
            _context.Set<UserModel>().Remove(userModel);
            await _context.SaveChangesAsync();
            return _mapper.Map<User>(userModel);
        }
        else
        {   
            throw new QueryException("User with Id " + UserId.ToString() + " does not exist in databse");
        }
    }

    public async Task<User> FindUserAsync(int UserId)
    {
        UserModel? userModel = await _context.Set<UserModel>().FindAsync(UserId);

        if (userModel is not null)
        {
            return _mapper.Map<User>(userModel);
        }
        else
        {
            throw new QueryException("User with Id " + UserId.ToString() + " does not exist in databse");
        }
    }

    public async Task<User> FindUserByNameAsync(string userName)
    {
        UserModel? userModel = await _context.Set<UserModel>().FirstOrDefaultAsync(
                                      user => user.userName == userName);

        if(userModel is not null)
        {
            return _mapper.Map<User>(userModel);
        }
        else
        {
            throw new QueryException("user with name " + userName + " does not " +
                                     "exist in database");
        }

    }

    public async Task<bool> ContainsUserAsync(string userName)
    {
        UserModel? userModel = await _context.Set<UserModel>().FirstOrDefaultAsync(
                                      user => user.userName == userName);

        return userModel is not null;
    }

    public async Task<User> GetUserByNameAsync(string userName)
    {
        UserModel? userModel = await _context.Set<UserModel>().FirstOrDefaultAsync(
                                      user => user.userName == userName);

        if(userModel is not null)
        {
            return _mapper.Map<User>(userModel);
        }
        else
        {
            throw new QueryException(userName + " does not exist in database");
        }
    }
}
