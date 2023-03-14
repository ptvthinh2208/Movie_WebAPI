using MovieService.Business.DTOs;
using MovieService.Domain.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieService.Business.Mapper
{
    public class MovieMapper
    {
        public static Movie MapFromDtoToMovie(MovieDto movieDto)
        {
            return new Movie()
            {
                MovieId = movieDto.MovieId,
                MovieName = movieDto.MovieName,
                Title = movieDto.Title,
                Date = movieDto.Date,
                Time = movieDto.Time,

            };
        }

        public static MovieDto MapFromMovieToDto(Movie movie)
        {
            return new MovieDto()
            {
                MovieId = movie.MovieId,
                ActorName = movie.Actor?.ActorName,
                CountryName = movie.Country?.CountryName,
                DirectorName = movie.Director?.DirectorName,
                GenreName = movie.Genre?.GenreName,
                Date = movie.Date,
                MovieName = movie.MovieName,
                Time = movie.Time,
                Title = movie.Title

            };
        }
    }
}
