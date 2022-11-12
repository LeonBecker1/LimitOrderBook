using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimitOrderBook.Application.Services.Authentication;

public interface IAuthenticationService
{
    Task AuthenticateLogin(string userName, string password);

    Task AuthenticateRegister(string userName, string password);

    Task PerformRegistration(string userName, string password);
}
