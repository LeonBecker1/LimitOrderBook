using LimitOrderBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimitOrderBook.Infrastructure.Persistence.Models;

public class StockModel
{
    
    public StockModel(int stockId, string abbreviation)
    {
        this.stockId      = stockId;
        this.abbreviation = abbreviation;
    }

    public StockModel()
    {

    }

    [Key]
    [Column("Stock_Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int stockId { get; set; }

    [Required]
    [Column("Abbreviation", TypeName = "Varchar(8)")]
    public string abbreviation { get; set; } = null!; 
}
