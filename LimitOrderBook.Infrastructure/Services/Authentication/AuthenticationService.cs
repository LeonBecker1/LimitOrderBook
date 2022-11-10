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
        _unitofWork       = unitofWork;
    }

    public async Task AuthenticateLogin(string userName, string password)
    {
        try
        {
            User user = await _unitofWork.Users.FindUserByNameAsync(userName);
            if(user.userName != userName)
            {
                throw new ApplicationException("User " + userName +
                                               " has different password");
            }
        }
        catch (QueryException)
        {
            throw new AuthenticationException("User with userName " + userName
                                              + " does not exist in database");
        }
    }

    public Task AuthenticateRegister(string userName, string password)
    {
        
    }
}
