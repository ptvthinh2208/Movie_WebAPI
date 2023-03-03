using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieService.Domain.Director
{
    public class Director
    {
        public Director()
        {
            Movie = new HashSet<Movie.Movie>();
        }
        public int DirectorId { get; set; }
        public string DirectorName { get; set; }
        public ICollection<Movie.Movie> Movie { get; set; }
    }
}
