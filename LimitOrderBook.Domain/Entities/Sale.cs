using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimitOrderBook.Domain.Entities;

public class Sale
{

    public Sale(Stock underlying, User buyer, User seller)
    {
        this.underlying = underlying;
        this.buyer      = buyer;
        this.seller     = seller;
    }

    public int saleId { get; set; }

    public Stock underlying { get; set; } = null!;

    public User buyer { get; set; } = null!;

    public User seller { get; set; } = null!;

}
