using Microsoft.EntityFrameworkCore;
using MovieService.Domain.Actor;
using MovieService.Domain.Director;
using MovieService.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;

namespace MovieService.Infrastructure.Repositories
{
    public class DirectorRepository : BasePropertyRepository<Director>
    {
        private MovieContext _movieContext;
        public DirectorRepository(MovieContext movieContext) : base(movieContext)
        {
            _movieContext = movieContext;
        }

        public override async Task<Director> GetByName(string name)
        {
            return await _movieContext.Director.FirstOrDefaultAsync(x => x.DirectorName == name);
        }

        public override async Task<Director> GetById(int id)
        {
            return await _movieContext.Director.FirstOrDefaultAsync(a => a.DirectorId == id);
        }
    }
}
