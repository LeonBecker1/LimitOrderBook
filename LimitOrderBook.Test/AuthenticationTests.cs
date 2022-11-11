using LimitOrderBook.Application.Exceptions;
using LimitOrderBook.Domain.Entities;
using LimitOrderBook.Infrastructure.Services.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimitOrderBook.Test;

public class Authenticationtests
{
    [Fact]
    public async void LoginForNonExistantUserFails()
    {
        var authService = await UtilityClass.GetAuthenticationServiceAsync();
        await Assert.ThrowsAsync<AuthenticationException>(() =>
                    authService.AuthenticateLogin("Leon", "HelloWorld1"));
    }

    [Fact]
    public async void LoginWithWrongPasswordFails()
    {
        var authService = await UtilityClass.GetAuthenticationServiceAsync();
        var unitofWork = await UtilityClass.GetUnitofWork();

        Portfolio portfolio = new Portfolio(new List<Position>());
        User user = new User("Leon", "HelloWorld1", 100, portfolio);
        await unitofWork.Users.AddUserAsync(user);

        await Assert.ThrowsAsync<AuthenticationException>(() =>
                     authService.AuthenticateLogin("Leon", "HelloWorld"));

    }

    [Fact]
    public async void CorrectLoginWorks()
    {
        var authService = await UtilityClass.GetAuthenticationServiceAsync();
        var unitofWork = await UtilityClass.GetUnitofWork();

        Portfolio portfolio = new Portfolio(new List<Position>());
        User user = new User("Leon", "HelloWorld1", 100, portfolio);
        await unitofWork.Users.AddUserAsync(user);

        await authService.AuthenticateLogin("Leon", "HelloWorld1");
      
    }

    public async void RegistrationWithUnavailableUserNameFails()
    {
        var authService = await UtilityClass.GetAuthenticationServiceAsync();
        var unitofWork = await UtilityClass.GetUnitofWork();

        Portfolio portfolio = new Portfolio(new List<Position>());
        User user = new User("Leon", "HelloWorld1", 100, portfolio);
        await unitofWork.Users.AddUserAsync(user);
    }

    [Fact]
    public async void CorrectRegistrationWorks()
    {
        var authService = await UtilityClass.GetAuthenticationServiceAsync();
        await authService.AuthenticateRegister("Leon", "HelloWorld1");
    }
}
