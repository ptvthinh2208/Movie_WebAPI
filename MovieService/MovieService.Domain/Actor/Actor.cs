using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieService.Domain.Actor
{
    public class Actor
    {
        public Actor()
        {
            Movie = new HashSet<Movie.Movie>();
        }
        public int ActorId { get; set; }
        public string ActorName { get; set; }
        public ICollection<Movie.Movie> Movie { get; set; }
    }
}
