using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimitOrderBook.Domain.Entities;

public class Stock
{

    public Stock(string abbreviation)
    {
        this.abbreviation = abbreviation;
    }

    public int stockId { get; set; }

    public string abbreviation { get; set; } = null!;
}
