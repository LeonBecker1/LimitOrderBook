using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LimitOrderBook.Domain.Entities;

namespace LimitOrderBook.Application.Persistence;

public interface ISaleRepository : IRepository<Sale>
{
}
