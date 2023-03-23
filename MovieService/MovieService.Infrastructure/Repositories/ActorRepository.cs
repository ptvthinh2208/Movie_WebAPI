using MovieService.Domain.Actor;
using Microsoft.EntityFrameworkCore;
using MovieService.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieService.Infrastructure.Repositories
{
    public class ActorRepository : BasePropertyRepository<Actor>
    {
        private readonly MovieContext _movieContext;
        public ActorRepository(MovieContext movieContext) : base(movieContext)
        {
            _movieContext = movieContext;
        }
        public override async Task<Actor> GetById(int id)
        {
            return await _movieContext.Actor.FirstOrDefaultAsync(a => a.ActorId == id);
        } 
        public override async Task<Actor> GetByName(string name)
        {
            return await _movieContext.Actor.FirstOrDefaultAsync(a => a.ActorName == name);
        }

        
    }
}
