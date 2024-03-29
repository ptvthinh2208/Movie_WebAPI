﻿using Microsoft.EntityFrameworkCore;
using MovieService.Domain;
using MovieService.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieService.Infrastructure.Repositories
{
    public class BasePropertyRepository<T> : IPropertyRepository<T> where T : class
    {
        private readonly MovieContext _movieContext;

        public BasePropertyRepository(MovieContext movieContext)
        {
            _movieContext = movieContext;
        }

        public async Task<IEnumerable<T>> Get()
        {
            return await _movieContext.Set<T>().ToArrayAsync();
        }

        public virtual Task<T> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task Create(T entity)
        {
            await _movieContext.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            _movieContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _movieContext.Set<T>().Remove(entity);
        }

        public virtual Task<T> GetByName(string name)
        {
            throw new NotImplementedException();
        }

        
    }
}
