using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LimitOrderBook.Domain.Entities;

namespace LimitOrderBook.Application.Persistence;

public interface IUserRepository : IRepository<User>
{
    Task<User> ChangeBalanceAsync(decimal BalanceDelta, int UserId);

    Task<User> FindUserAsync(int UserId);

    Task<User> FindUserByNameAsync(string userName);

    Task<User> AddUserAsync(User user);

    Task<bool> ContainsUserAsync(string userName);

    Task<User> GetUserByNameAsync(string userName);
}
