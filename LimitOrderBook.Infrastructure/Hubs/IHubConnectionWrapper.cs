using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimitOrderBook.Infrastructure.Hubs;

public interface IHubConnectionWrapper
{
    Task SendAsync(string methodName, params object[] parameters);
}
