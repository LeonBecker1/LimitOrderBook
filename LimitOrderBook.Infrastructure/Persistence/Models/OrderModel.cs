using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LimitOrderBook.Domain.Entities;

namespace LimitOrderBook.Infrastructure.Persistence.Models;

public class OrderModel
{
    
    public OrderModel(int orderId, int price, uint quantity, StockModel underlying, UserModel issuer, bool isBuyOrder)
    {
        this.orderId    = orderId;
        this.price      = price;
        this.quantity   = quantity;
        this.underlying = underlying;
        this.issuer     = issuer;
        this.IsBuyOrder = isBuyOrder;
    }

    public OrderModel()
    {

    }

    [Key]
    [Column("Order_Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int orderId { get; set; }

    [Required]
    [Column("Price")]
    public int price { get; set; }

    [Required]
    [Column("Quantity")]
    public uint quantity { get; set; }

    [Required]
    [ForeignKey("Underlying_Id")]
    public StockModel underlying { get; set; } = null!;

    [Required]
    [ForeignKey("Issuer_Id")]
    public UserModel issuer { get; set; } = null!;

    [Required]
    [Column("Is_Buy_Order", TypeName = "BIT")]
    public bool IsBuyOrder { get; set; }
}
