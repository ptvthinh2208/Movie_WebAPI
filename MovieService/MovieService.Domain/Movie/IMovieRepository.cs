using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieService.Domain.Movie
{
    public interface IMovieRepository
    {
        Task<Movie> GetByName(string name);
        Task<IEnumerable<Movie>> Get();
        Task<Movie> Get(int id);
        Task Create(Movie entity);
        void Update(Movie entity);
        void Delete(Movie entity);
    }
}
