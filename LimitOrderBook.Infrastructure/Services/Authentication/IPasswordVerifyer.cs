using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimitOrderBook.Infrastructure.Services.Authentication;

public interface IPasswordVerifyer
{
    bool PasswordIsValid(string password);
}
