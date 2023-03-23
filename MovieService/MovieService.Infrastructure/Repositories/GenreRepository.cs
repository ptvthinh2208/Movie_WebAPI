using Microsoft.EntityFrameworkCore;
using MovieService.Domain.Actor;
using MovieService.Domain.Genre;
using MovieService.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieService.Infrastructure.Repositories
{
    public class GenreRepository : BasePropertyRepository<Genre>
    {

        private readonly MovieContext _movieContext;

        public GenreRepository(MovieContext movieContext) : base(movieContext)
        {
            _movieContext = movieContext;
        }
        public override Task<Genre> GetByName(string name)
        {
            return _movieContext.Genre.FirstOrDefaultAsync(x => x.GenreName == name);
        }
        public override async Task<Genre> GetById(int id)
        {
            return await _movieContext.Genre.FirstOrDefaultAsync(a => a.GenreId == id);
        }
    }
}
