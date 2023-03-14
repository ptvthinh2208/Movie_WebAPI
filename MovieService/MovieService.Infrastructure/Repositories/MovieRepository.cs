using Microsoft.EntityFrameworkCore;
using MovieService.Domain.Movie;
using MovieService.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieService.Infrastructure.Repositories
{
    public class MovieRepository : BasePropertyRepository<Movie>
    {
        private readonly MovieContext _movieContext;
        public MovieRepository(MovieContext movieContext) : base(movieContext)
        {
            _movieContext = movieContext;
        }
        public override async Task<Movie> GetByName(string name)
        {
            return await _movieContext.Movie?.FirstOrDefaultAsync(x => x.MovieName == name);
        }

        public override async Task<IEnumerable<Movie>> GetByNames(string[] names)
        {
            return await _movieContext.Movie.Where(a => names.Any(x => x == a.MovieName)).ToArrayAsync();
        }
    }
}
