using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieService.Domain.Movie
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetMovies(MovieFilterModel movieFilterModel);
        Task<Movie> GetMoviesByNameVersion(MovieFilterModel movieFilterModel);
        Task<IEnumerable<Movie>> GetMoviesByfilterModels(
            IEnumerable<MovieFilterModel> movieFilterModels);
        Task<Movie> GetMovieById(int movieId);
        Task InsertMovie(Movie movie);
        void UpdateMovie(Movie movie);
        Task BulkUpdateMovie(IEnumerable<Movie> movies);
    }
}
