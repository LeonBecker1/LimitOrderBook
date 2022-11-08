using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimitOrderBook.Domain.Entities;

public class User
{

    public User(string userName, string password, decimal balance, Portfolio portfolio)
    {
        this.userName  = userName;
        this.password  = password;
        this.balance   = balance;
        this.portfolio = portfolio;
    }

    public int userId { get; set; }

    public string userName { get; set; } = null!;

    public string password { get; set; } = null!;

    public decimal balance { get; set; }

    public Portfolio portfolio { get; set; } = null!;


}
