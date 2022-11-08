using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimitOrderBook.Infrastructure.Persistence.Models;

public class PortfolioModel
{
    
    public PortfolioModel(int portfolioId, List<PositionModel> positions)
    {
        this.portfolioId = portfolioId;
        this.positions   = positions;
    } 

    public PortfolioModel()
    {

    }

    [Key]
    [Column("Portfolio_Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int portfolioId { get; set; }


    [Required]
    [ForeignKey("Portfolio_Id")]
    public List<PositionModel> positions { get; set; } = null!;


}
