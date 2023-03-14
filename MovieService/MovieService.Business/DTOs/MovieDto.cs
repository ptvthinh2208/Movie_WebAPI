using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieService.Business.DTOs
{
    public class MovieDto
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public int? Date { get; set; }
        public int? Time { get; set; }
        public string MovieName { get; set; }
        public string ActorName { get; set; }
        public string DirectorName { get; set; }
        public string CountryName { get; set; }
        public string GenreName { get; set; }

    }
}
