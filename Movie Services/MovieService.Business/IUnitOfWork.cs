using MovieService.Domain.Genre;
using MovieService.Domain;
using MovieService.Domain.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using MovieService.Domain.Actor;
using MovieService.Domain.Country;
using MovieService.Domain.Director;

namespace MovieService.Business
{
    public interface IUnitOfWork
    {
        IMovieRepository MovieRepository { get; }
        IPropertyRepository<Actor> ActorRepository { get; }
        IPropertyRepository<Country> CountryRepository { get; }
        IPropertyRepository<Director> DirectorRepository { get; }
        IPropertyRepository<Genre> GenreRepository { get; }
        void Save();
    }
}
