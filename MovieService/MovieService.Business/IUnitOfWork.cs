using MovieService.Domain;
using MovieService.Domain.Actor;
using MovieService.Domain.Country;
using MovieService.Domain.Director;
using MovieService.Domain.Genre;
using MovieService.Domain.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieService.Business
{
    public interface IUnitOfWork
    {
        IPropertyRepository<Actor> ActorRepository { get; }
        IPropertyRepository<Country> CountryRepository { get; }
        IPropertyRepository<Director> DirectorRepository { get; }
        IPropertyRepository<Genre> GenreRepository { get; }
        IPropertyRepository<Movie> MovieRepository { get; }
        void Save();
    }
}
