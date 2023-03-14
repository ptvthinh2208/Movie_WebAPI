using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieService.Domain.Genre
{
    public class Genre
    {
        public Genre()
        {
            Movie = new HashSet<Movie.Movie>();
        }
        public int GenreId { get; set; }
        public string GenreName { get; set; }
        public ICollection<Movie.Movie> Movie { get; set; }
    }
}
