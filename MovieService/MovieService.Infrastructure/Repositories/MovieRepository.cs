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

        public async Task<IEnumerable<Movie>> GetByNames(string[] names)
        {
            return await _movieContext.Movie.Where(a => names.Any(n => n == a.MovieName))
                .ToArrayAsync();
        }
        public async Task<Movie> Get(string id)
        {
            return await _movieContext.Movie.FindAsync(id);
        }
        public async Task<Movie> Get(int id)
        {
            return await _movieContext.Movie.FindAsync(id);
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
