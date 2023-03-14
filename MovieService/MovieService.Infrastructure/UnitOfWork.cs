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
using MovieService.Domain.Country;
using MovieService.Domain.Director;
using MovieService.Domain.Genre;

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
        private IPropertyRepository<Country> _countryRepository;
        private IPropertyRepository<Director> _directorRepository;
        private IPropertyRepository<Genre> _genreRepository;


        public IPropertyRepository<Actor> ActorRepository => _actorRepository = new ActorRepository(_movieContext);
        public IPropertyRepository<Country> CountryRepository => _countryRepository = new CountryRepository(_movieContext);
        public IPropertyRepository<Director> DirectorRepository => _directorRepository = new DirectorRepository(_movieContext);
        public IPropertyRepository<Genre> GenreRepository => _genreRepository = new GenreRepository(_movieContext);

        public void Save()
        {
            _movieContext.SaveChanges();
        }
    }
}
