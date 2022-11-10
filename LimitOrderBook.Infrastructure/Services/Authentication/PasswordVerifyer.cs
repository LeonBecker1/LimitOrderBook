using LimitOrderBook.Infrastructure.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimitOrderBook.Infrastructure.Services.Authentication;

public class PasswordVerifyer : IPasswordVerifyer
{

    private readonly PasswordOptions _passwordOptions;

    public PasswordVerifyer(IOptions<PasswordOptions> passwordOptions)
    {
        _passwordOptions = passwordOptions.Value;
    }

    public bool PasswordIsValid(string password)
    {
        throw new NotImplementedException();
    }
}
