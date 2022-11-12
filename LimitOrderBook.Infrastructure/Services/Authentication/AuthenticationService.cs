using LimitOrderBook.Application.Exceptions;
using LimitOrderBook.Application.Persistence;
using LimitOrderBook.Application.Services.Authentication;
using LimitOrderBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimitOrderBook.Infrastructure.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{

    private readonly IPasswordVerifyer _passwordVerifyer;

    private readonly IUnitofWork _unitofWork;

    public AuthenticationService(IPasswordVerifyer passwordVerifyer, IUnitofWork unitofWork)
    {
        _passwordVerifyer = passwordVerifyer;
        _unitofWork = unitofWork;
    }

    public async Task AuthenticateLogin(string userName, string password)
    {
        if (!await _unitofWork.Users.ContainsUserAsync(userName))
        {
            throw new AuthenticationException(userName + " is not a registered user");
        }

        User user = await _unitofWork.Users.FindUserByNameAsync(userName);

        if (user.password != password)
        {
            throw new AuthenticationException(userName + " uses different password");
        }
    }

    public async Task AuthenticateRegister(string userName, string password)
    {
        if (!_passwordVerifyer.PasswordIsValid(password))
        {
            throw new AuthenticationException(password + " has insuficient complexity");
        }

        if (await _unitofWork.Users.ContainsUserAsync(userName))
        {
            throw new AuthenticationException(userName + " is already a registered user");
        }
    }

    public async Task PerformRegistration(string userName, string password)
    {
        Portfolio portfolio = new Portfolio(new List<Position>());
        User user = new User(userName, password, 5000, portfolio);

        await _unitofWork.Users.AddUserAsync(user);
    }
}
