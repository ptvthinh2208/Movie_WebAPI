using MovieService.Business.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieService.Business.Common
{
    public class Validator
    {
        public static bool ValidateMovieDto(MovieDto movieDto)
        {
            if (String.IsNullOrEmpty(movieDto.ActorName) && String.IsNullOrEmpty(movieDto.DirectorName) && String.IsNullOrEmpty(movieDto.CountryName) && String.IsNullOrEmpty(movieDto.GenreName))
            {
                return false;
            }
            return true;
        }
    }
}
