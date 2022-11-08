using LimitOrderBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimitOrderBook.Infrastructure.Persistence.Models;

public class PositionModel
{
    
    public PositionModel(int positionId, StockModel underlying, uint quantity)
    {
        this.positionId = positionId;
        this.underlying = underlying;
        this.quantity   = quantity;
    }

    public PositionModel()
    {

    }

    [Key]
    [Column("Position_Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int positionId { get; set; }

    [Required]
    [ForeignKey("Underlying_Id")]
    public StockModel underlying { get; set; } = null!;

    [Required]
    [Column("Quantity")]
    public uint quantity { get; set; }
}
