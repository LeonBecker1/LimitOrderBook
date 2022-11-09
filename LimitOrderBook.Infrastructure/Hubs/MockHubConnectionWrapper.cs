using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimitOrderBook.Infrastructure.Hubs;

public class MockHubConnectionWrapper : IHubConnectionWrapper
{
    public Task SendAsync(string methodName, params object[] parameters)
    {
        return Task.CompletedTask;
    }
}
