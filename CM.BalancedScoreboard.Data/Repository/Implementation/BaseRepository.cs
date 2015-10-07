﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CM.BalancedScoreboard.Data.Repository.Abstract;
using CM.BalancedScoreboard.Domain.Abstract;

namespace CM.BalancedScoreboard.Data.Repository.Implementation
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IEntity
    {
        readonly IDbContext _context;

        public BaseRepository() { } 

        public BaseRepository(IUnitOfWork uof)
        {
            _context = uof.Context;
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null)
        {
            if (filter != null)
                return _context.Set<TEntity>().Where(filter);
            else
                return _context.Set<TEntity>();
        }

        public TEntity Single(Expression<Func<TEntity, bool>> filter)
        {
            return _context.Set<TEntity>().SingleOrDefault(filter);
        }

        public async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            var i = await _context.Set<TEntity>().SingleOrDefaultAsync(filter);
            System.Console.WriteLine(i.Id);
            return i;
        }

        public virtual void Add(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                _context.Set<TEntity>().Add(entity);
            }
        }

        public virtual void Update(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                _context.Set<TEntity>().Attach(entity);
                _context.SetModified(entity);
            }
        }

        public virtual void Delete(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                _context.Set<TEntity>().Remove(entity);
            }
        }
    }
}
