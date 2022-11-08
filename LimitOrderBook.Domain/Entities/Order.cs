using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimitOrderBook.Domain.Entities;

public class Order
{
    public Order(int price, uint quantity, Stock underlying, User issuer, bool isBuyOrder)
    {
        this.price      = price;
        this.quantity   = quantity;
        this.underlying = underlying;
        this.issuer     = issuer;
        this.isBuyOrder = isBuyOrder;
    }

    public int orderId { get; set; }

    public int price { get; set;}

    public uint quantity { get; set;}

    public Stock underlying { get; set; } = null!;

    public User issuer { get; set; } = null!;

    public bool isBuyOrder { get; set; }
}
