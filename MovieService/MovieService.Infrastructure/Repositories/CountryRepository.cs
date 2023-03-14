using Microsoft.EntityFrameworkCore;
using MovieService.Domain.Country;
using MovieService.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieService.Infrastructure.Repositories
{
    public class CountryRepository : BasePropertyRepository<Country>
    {
        private readonly MovieContext _movieContext;
        public CountryRepository(MovieContext movieContext) : base(movieContext)
        {
            _movieContext = movieContext;
        }
        public override async Task<Country> GetByName(string name)
        {
            return await _movieContext.Country.FirstOrDefaultAsync(x => x.CountryName == name);
        }

        public virtual async Task<IEnumerable<Country>> GetByNames(string[] names)
        {
            return await _movieContext.Country.Where(a => names.Any(x => x == a.CountryName)).ToArrayAsync();
        }
    }
}
