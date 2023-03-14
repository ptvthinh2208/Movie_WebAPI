using MovieService.Business;
using MovieService.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieService.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MovieContext _movieContext;
        public UnitOfWork(MovieContext movieContext)
        {
            _movieContext = movieContext;
        }
        public void Save()
        {
            _movieContext.SaveChanges();
        }
    }
}
