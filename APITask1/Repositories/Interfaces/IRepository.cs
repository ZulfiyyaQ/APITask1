﻿using System.Linq.Expressions;

namespace APITask1.Repositories.Interfaces
{
    public interface IRepository<T> where T:BaseEntity,new()
    {
        IQueryable<T> GetAllAsync(Expression<Func<T, bool>>? expression=null,
            Expression<Func<T,object>>? orderExpression=null,
             bool isDescenting = false,
             int skip=0,
             int take=0,
             bool isTracking=true,
            params string[] include);
        Task<T> GetByIdAsync(int id);

        Task AddAsync(T entity);
        void Update(T entity);

        void Delete(T entity);
        Task SaveChangesAsync();

    }
}
