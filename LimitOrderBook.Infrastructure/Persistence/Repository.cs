using LimitOrderBook.Application.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using LimitOrderBook.Application.Exceptions;
using AutoMapper;

namespace LimitOrderBook.Infrastructure.Persistence;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{

    private readonly DbContext _dbContext;

    private readonly IMapper _mapper;

    public Repository(DbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    /*
    public async Task<TEntity> Add(TEntity entity)
    {
        _dbContext.Set<TEntity>().Add(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity> FindAsync(int Id)
    {
        TEntity? entity = await _dbContext.Set<TEntity>().FindAsync(Id);

        if(entity is not null)
        {
            return entity;
        }
        else
        {
            throw new InvalidOperationException(typeof(TEntity).Name + "with id " + Id.ToString() + " not found in database");
        }

    }

    public async Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbContext.Set<TEntity>().Where(predicate).ToListAsync();
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        return  await _dbContext.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity> Remove(TEntity entity)
    {
        _dbContext.Set<TEntity>().Remove(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    } */

}
