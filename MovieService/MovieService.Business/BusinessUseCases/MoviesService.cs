using MovieService.Business.Common;
using MovieService.Business.DTOs;
using MovieService.Business.Mapper;
using MovieService.Domain.Actor;
using MovieService.Domain.Country;
using MovieService.Domain.Director;
using MovieService.Domain.Genre;
using MovieService.Domain.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieService.Business.BusinessUseCases
{
    public class MoviesService : IMovieService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MoviesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<MovieResponeDto>> GetMovies()
        {
            var entityEnum = await _unitOfWork.MovieRepository.Get();
            var entities = entityEnum as Movie[];
            if (entities == null)
            {
                return new MovieResponeDto[0];
            }
            List<MovieResponeDto> entityResponseDtos = new List<MovieResponeDto>();
            foreach (var entity in entities)
            {
                entityResponseDtos.Add(new MovieResponeDto()
                {
                    MovieId = entity.MovieId,
                    MovieName = entity.MovieName,
                    Title = entity.Title
                });

            }
            return entityResponseDtos;
        }
        public async Task<MovieDto> GetMovieById(string movieId)
        {
            var isValidId = Int32.TryParse(movieId, out var id);
            if (!isValidId)
            {
                return null;
            }
            var movie = await _unitOfWork.MovieRepository.Get(id);
            if (movie != null)
            {
                var actor = await _unitOfWork.ActorRepository.GetById(movie.ActorId);
                var director = await _unitOfWork.DirectorRepository.GetById(movie.DirectorId);
                var country = await _unitOfWork.CountryRepository.GetById(movie.CountryId);
                var genre = await _unitOfWork.GenreRepository.GetById(movie.GenreId);
                return new MovieDto()
                {
                    MovieId = movie.MovieId,
                    MovieName = movie.MovieName,
                    Title = movie.Title,
                    Date = movie.Date,
                    Time = movie.Time,
                    ActorName = actor.ActorName,
                    DirectorName = director.DirectorName,
                    CountryName = country.CountryName,
                    GenreName = genre.GenreName

                };
            }
            return null;
        }
        public async Task<ReturnResult> CreateMovie(MovieDto movieDto)
        {
            try
            {

                var loadMovieResult = await LoadMoviePropertiesForCreate(movieDto);

                if (loadMovieResult.IsUpdatedSuccess)
                {
                    var movieUpdate = loadMovieResult.data as Movie;
                    return new ReturnResult()
                    {
                        IsUpdatedSuccess = true,
                        data = MovieMapper.MapFromMovieToDto(movieUpdate)
                    };
                }

                if (!loadMovieResult.IsSuccess)
                {
                    return loadMovieResult;
                }

                var movie = loadMovieResult.data as Movie;

                await _unitOfWork.MovieRepository.Create(movie);
                _unitOfWork.Save();

                return new ReturnResult()
                {
                    IsSuccess = true,
                    data = MovieMapper.MapFromMovieToDto(movie)
                };

            }
            catch (Exception ex)
            {
                return new ReturnResult()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
        public async Task<ReturnResult> UpdateMovieId(string strMovieId, MovieDto movieDto)
        {
            try
            {
                var loadPropertiesResult =
                    await LoadMoviePropertiesForUpdate(strMovieId, movieDto, null, null, null, null);
                if (!loadPropertiesResult.IsSuccess)
                {
                    return loadPropertiesResult;
                }

                var movie = loadPropertiesResult.data as Movie;
                _unitOfWork.MovieRepository.Update(movie);
                _unitOfWork.Save();
                return new ReturnResult()
                {
                    IsSuccess = true,
                    data = MovieMapper.MapFromMovieToDto(movie)
                };
            }
            catch (Exception ex)
            {
                return new ReturnResult()
                {
                    IsSuccess = false,
                    data = ex.Message
                };
            }
        }
        //Check Movie Properties for Create
        public async Task<ReturnResult> LoadMoviePropertiesForCreate(MovieDto movieDto)
        {
            var movie = MovieMapper.MapFromDtoToMovie(movieDto);
            if (!Validator.ValidateMovieDto(movieDto))
            {
                return new ReturnResult()
                {
                    IsSuccess = false,
                    Message = ErrorMessage.InvalidObject()
                };
            }

            if (!String.IsNullOrEmpty(movieDto.ActorName))
            {
                var actor = await _unitOfWork.ActorRepository.GetByName(movieDto.ActorName);
                if (actor == null)
                {
                    return new ReturnResult()
                    {
                        IsSuccess = false,
                        Message = ErrorMessage.FailInsert_PropertyNotExist("Actor")
                    };
                }

                movie.ActorId = actor.ActorId;
                movie.Actor = actor;
            }

            if (!String.IsNullOrEmpty(movieDto.CountryName))
            {
                var country = await _unitOfWork.CountryRepository.GetByName(movieDto.CountryName);
                if (country == null)
                {
                    return new ReturnResult()
                    {
                        IsSuccess = false,
                        Message = ErrorMessage.FailInsert_PropertyNotExist("Country")
                    };
                }

                movie.CountryId = country.CountryId;
                movie.Country = country;
            }

            if (!String.IsNullOrEmpty(movieDto.DirectorName))
            {
                var director = await _unitOfWork.DirectorRepository.GetByName(movieDto.DirectorName);
                if (director == null)
                {
                    return new ReturnResult()
                    {
                        IsSuccess = false,
                        Message = ErrorMessage.FailInsert_PropertyNotExist("Director")
                    };
                }

                movie.DirectorId = director.DirectorId;
                movie.Director = director;
            }

            if (!String.IsNullOrEmpty(movieDto.GenreName))
            {
                var genre = await _unitOfWork.GenreRepository.GetByName(movieDto.GenreName);
                if (genre == null)
                {
                    return new ReturnResult()
                    {
                        IsSuccess = false,
                        Message = ErrorMessage.FailInsert_PropertyNotExist("Genre")
                    };
                }

                movie.GenreId = genre.GenreId;
                movie.Genre = genre;
            }

            var filterMovie = new MovieDto()
            {
                ActorName = movieDto.ActorName,
                CountryName = movieDto.CountryName,
                DirectorName = movieDto.DirectorName,
                GenreName = movieDto.GenreName
            };

            return new ReturnResult()
            {
                IsSuccess = true,
                data = movie
            };

        }
        //Check Movie Proerties for Update
        public async Task<ReturnResult> LoadMoviePropertiesForUpdate(string movieId,
            MovieDto movieDto, Actor[] actors, Country[] countries, Director[] directors,
            Genre[] genres)
        {
            var id = movieDto.MovieId;
            if (movieId != null)
            {
                var isValidId = Int32.TryParse(movieId, out id);
                if (!isValidId)
                {
                    return new ReturnResult()
                    {
                        IsSuccess = false,
                        Message = ErrorMessage.InvalidId()
                    };
                }
            }

            var movie = await _unitOfWork.MovieRepository.Get(id);

            if (movie == null)
            {
                return new ReturnResult()
                {
                    IsSuccess = false,
                    Message = ErrorMessage.FailUpdate_PropertyNotExist("Movie")
                };
            }

            if (movieDto.ActorName != null)
            {
                if (movieDto.ActorName == String.Empty)
                {
                    movie.Actor = null;
                    movie.ActorId = 0;
                }
                else
                {
                    Actor actor = null;
                    if (actors == null)
                    {
                        actor = await _unitOfWork.ActorRepository.GetByName(movieDto.ActorName);
                    }
                    else
                    {
                        actor =
                            actors.FirstOrDefault(a => a.ActorName == movieDto.ActorName);
                    }
                    if (actor == null)
                    {
                        return new ReturnResult()
                        {
                            IsSuccess = false,
                            Message = ErrorMessage.FailUpdate_PropertyNotExist("Actor")
                        };
                    }

                    movie.ActorId = actor.ActorId;
                    movie.Actor = actor;
                }
            }

            if (movieDto.CountryName != null)
            {
                if (movieDto.CountryName == String.Empty)
                {
                    movie.CountryId = 0;
                    movie.Country = null;
                }
                else
                {
                    Country country = null;
                    if (countries == null)
                    {
                        country = await _unitOfWork.CountryRepository.GetByName(movieDto.CountryName);
                    }
                    else
                    {
                        country = countries.FirstOrDefault(e => e.CountryName == movieDto.CountryName);
                    }
                    if (country == null)
                    {
                        return new ReturnResult()
                        {
                            IsSuccess = false,
                            Message = ErrorMessage.FailUpdate_PropertyNotExist("Edition")
                        };
                    }

                    movie.CountryId = country.CountryId;
                    movie.Country = country;
                }
            }

            if (movieDto.DirectorName != null)
            {
                if (movieDto.DirectorName == String.Empty)
                {
                    movie.DirectorId = 0;
                    movie.Director = null;
                }
                else
                {
                    Director director = null;
                    if (directors == null)
                    {
                        director = await _unitOfWork.DirectorRepository.GetByName(movieDto.DirectorName);
                    }
                    else
                    {
                        director = directors.FirstOrDefault(v => v.DirectorName == movieDto.DirectorName);
                    }

                    if (director == null)
                    {
                        return new ReturnResult()
                        {
                            IsSuccess = false,
                            Message = ErrorMessage.FailUpdate_PropertyNotExist("Version")
                        };
                    }

                    movie.DirectorId = director.DirectorId;
                    movie.Director = director;
                }
            }

            if (movieDto.GenreName != null)
            {
                if (movieDto.GenreName == String.Empty)
                {
                    movie.Genre = null;
                    movie.GenreId = 0;
                }
                else
                {
                    Genre genre = null;
                    if (genres == null)
                    {
                        genre = await _unitOfWork.GenreRepository.GetByName(movieDto.GenreName);
                    }
                    else
                    {
                        genre = genres.FirstOrDefault(s => s.GenreName == movieDto.GenreName);
                    }
                    if (genre == null)
                    {
                        return new ReturnResult()
                        {
                            IsSuccess = false,
                            Message = ErrorMessage.FailUpdate_PropertyNotExist("Genre")
                        };
                    }

                    movie.GenreId = genre.GenreId;
                    movie.Genre = genre;
                }
            }

            if (movieDto.MovieName != null)
            {
                movie.MovieName = movieDto.MovieName;
                movie.Date = movieDto.Date;
                movie.Time = movieDto.Time;
                movie.Title = movieDto.Title;

            }

            if (true)
            {
                return new ReturnResult()
                {
                    IsSuccess = true,
                    data = movie
                };
            }

            return new ReturnResult()
            {
                IsSuccess = false,
                Message = ErrorMessage.FailUpdate_PropertiesValuesExist()
            };
        }
    }
}
