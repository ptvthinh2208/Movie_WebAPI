using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieService.Domain.Country
{
    public class Country
    {
        public Country()
        {
            Movie = new HashSet<Movie.Movie>();
        }
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public ICollection<Movie.Movie> Movie { get; set; }
    }
}
