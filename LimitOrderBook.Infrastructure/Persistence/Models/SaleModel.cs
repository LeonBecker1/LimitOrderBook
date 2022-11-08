using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimitOrderBook.Infrastructure.Persistence.Models;

public class SaleModel
{
    
    public SaleModel(int saleId, StockModel underlying, UserModel buyer, UserModel seller)
    {
        this.saleId     = saleId;
        this.underlying = underlying;
        this.buyer      = buyer;
        this.seller     = seller;
    }

    public SaleModel()
    {

    }

    [Key]
    [Column("Sale_Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int saleId { get; set; }

    [Required]
    [ForeignKey("Underlying_Id")]
    public StockModel underlying { get; set; } = null!;

    [Required]
    [ForeignKey("Buyer_Id")]
    public UserModel buyer { get; set; } = null!;

    [Required]
    [ForeignKey("Seller_Id")]
    public UserModel seller { get; set; } = null!;
}
