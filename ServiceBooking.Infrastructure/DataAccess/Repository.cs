using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ServiceBooking.Core.Interfaces;
using ServiceBooking.Infrastructure.Data;

namespace ServiceBooking.Infrastructure.DataAccess;

public class Repository<T>(DatabaseContext ctx) : IRepository<T> where T : class
{
      readonly DatabaseContext context = ctx;

      public void Add(T entity)
      {
            context.Set<T>().Add(entity);
      }

      public void Delete(T entity)
      {
            context.Set<T>().Remove(entity);
      }

      public async Task<IEnumerable<T>> FetchAsync(bool withNoTracking = false)
      {
            if (withNoTracking)
                  return await context.Set<T>().AsNoTracking().ToListAsync();
            else return await context.Set<T>().ToListAsync();
      }

      public async Task<IEnumerable<T>> FetchAsync(bool withNoTracking = false, params Expression<Func<T, object>>[] args)
      {
            IQueryable<T> query = context.Set<T>();

            foreach (var arg in args)
            {
                  query = query.Include(arg);
            }
            if (withNoTracking) return await query.AsNoTracking().ToListAsync();
            else return await query.ToListAsync();
      }

      public async Task<IEnumerable<T>> FetchAsync(Expression<Func<T, bool>> predicate, bool withNoTracking = false)
      {
            var query = context.Set<T>();

            if (withNoTracking)
                  return await query.Where(predicate).AsNoTracking().ToListAsync();
            else
                  return await query.Where(predicate).ToListAsync();
      }

      public async Task<IEnumerable<T>> FetchAsync(Expression<Func<T, bool>> predicate, bool withNoTracking = false, params Expression<Func<T, object>>[] args)
      {
            IQueryable<T> query = context.Set<T>();

            foreach (var arg in args)
            {
                  query = query.Include(arg);
            }
            if (withNoTracking)
                  return await query.Where(predicate).AsNoTracking().ToListAsync();
            else
                  return await query.Where(predicate).ToListAsync();
      }

      public async Task<T?> FindAsync(int id)
      {
            return await context.Set<T>().FindAsync(id);
      }

      public void Update(T entity)
      {
            context.Set<T>().Update(entity);
      }
}
