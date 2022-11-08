using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimitOrderBook.Domain.Entities;

public class Position
{
    public Position(Stock underlying, uint quantity)
    {
        this.underlying = underlying;
        this.quantity = quantity;
    }

    public int positionId { get; set; }

    public Stock underlying { get; set; } = null!;

    public uint quantity { get; set; }
}
