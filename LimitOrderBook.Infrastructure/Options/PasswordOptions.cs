using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimitOrderBook.Infrastructure.Options;

public class PasswordOptions
{

    public PasswordOptions(string[] passwordPatterns)
    {
        this.passwordPatterns = passwordPatterns;
    }

    public PasswordOptions()
    {

    }

    public String[] passwordPatterns { get; set; } = null!; 
}
