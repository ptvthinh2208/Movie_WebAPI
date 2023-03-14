using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieService.Domain.Movie
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string? Title { get; set; }
        public string? MovieName { get; set; }
        public int? Date { get; set; }
        public int? Time { get; set; }

        public int ActorId { get; set; }
        public int CountryId { get; set; }
        public int DirectorId { get; set; }
        public int GenreId { get; set; }

        public Actor.Actor Actor { get; set; }
        public Country.Country Country { get; set; }
        public Director.Director Director { get; set; }
        public Genre.Genre Genre { get; set; }

    }
}
