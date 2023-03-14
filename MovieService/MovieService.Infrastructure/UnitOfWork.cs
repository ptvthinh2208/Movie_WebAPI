using MovieService.Business;
using MovieService.Domain.Actor;
using MovieService.Domain;
using MovieService.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieService.Infrastructure.Repositories;

namespace MovieService.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MovieContext _movieContext;
        public UnitOfWork(MovieContext movieContext)
        {
            _movieContext = movieContext;
        }
        private IPropertyRepository<Actor> _actorRepository;

        public IPropertyRepository<Actor> ActorRepository => _actorRepository = new ActorRepository(_movieContext);
        public void Save()
        {
            _movieContext.SaveChanges();
        }
    }
}
