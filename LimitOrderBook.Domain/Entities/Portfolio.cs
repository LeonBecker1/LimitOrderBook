using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimitOrderBook.Domain.Entities;

public class Portfolio
{

    public Portfolio(List<Position> positions)
    {
        this.positions = positions;
    }

    public int portfolioId { get; set; }

    public List<Position> positions { get; set; } = null!;

}
