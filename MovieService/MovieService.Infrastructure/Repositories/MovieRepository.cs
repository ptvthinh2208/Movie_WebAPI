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
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieContext _movieContext;
        public MovieRepository(MovieContext movieContext)
        {
            _movieContext = movieContext;
        }
        public async Task<IEnumerable<Movie>> Get()
        {
            //DbSet<Movie> Movies
            return await _movieContext.Movie.ToArrayAsync();
        }
        public async Task<Movie> GetByName(string name)
        {
            return await _movieContext.Movie.FirstOrDefaultAsync(a => a.MovieName == name);
        }
        public async Task<Movie> Get(int movieId)
        {
            return await _movieContext.Movie.FirstOrDefaultAsync(f => f.MovieId == movieId);
        }
        public async Task Create(Movie entity)
        {
            await _movieContext.Movie.AddAsync(entity);
        }

        public void Update(Movie entity)
        {
            _movieContext.Movie.Update(entity);
        }

        public void Delete(Movie entity)
        {
            _movieContext.Movie.Remove(entity);
        }
    }
}
