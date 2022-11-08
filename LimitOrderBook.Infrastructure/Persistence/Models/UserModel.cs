using LimitOrderBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimitOrderBook.Infrastructure.Persistence.Models;

public class UserModel
{
    
    public UserModel(int userId, string userName, string password, decimal balance, PortfolioModel portfolio)
    {
        this.userId    = userId;
        this.userName  = userName;
        this.password  = password;
        this.balance   = balance;
        this.portfolio = portfolio;
    }

    public UserModel()
    {

    }

    [Key]
    [Column("User_Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int userId { get; set; }

    [Required]
    [Column("User_Name")]
    public string userName { get; set; } = null!;

    [Required]
    [Column("Password", TypeName = "Binary(64)")]
    public string password { get; set; } = null!;


    [Required]
    [Column("Balance", TypeName = "Decimal(6,2)")]
    public decimal balance { get; set; }


    [Required]
    [ForeignKey("Portfolio_Id")]
    public PortfolioModel portfolio { get; set; } = null!;
}
