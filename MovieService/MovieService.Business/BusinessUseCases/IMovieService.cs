using MovieService.Business.Common;
using MovieService.Business.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieService.Business.BusinessUseCases
{
    public interface IMovieService
    {
        Task<IEnumerable<MovieResponeDto>> GetMovies();
        Task<MovieDto> GetMovieById(string movieId);
        Task<ReturnResult> CreateMovie(MovieDto movie);
        Task<ReturnResult> UpdateMovieId(string movieId, MovieDto movieDto);

    }
}
