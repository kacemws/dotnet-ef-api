﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace API_2
{
    public class CRUDRepository<TEntity> : ICRUDRepository<TEntity> where TEntity : class
    {
        internal AppDbContext context;
        internal DbSet<TEntity> dbSet;

        public CRUDRepository(AppDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            IQueryable<TEntity> query = dbSet;

            return query.ToList();
            
        }

        public virtual TEntity GetByID(object id)
        {
            var entity = dbSet.Find(id);
            context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        
        public virtual void Create(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            dbSet.Update(entityToUpdate);
        }

        public void Save()
        {
            context.SaveChanges();
        }
        public void Dispose()
        {
            context.Dispose();
        }
    }
}