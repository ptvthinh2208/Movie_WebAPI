using Castle.Components.DictionaryAdapter;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using MovieService.Business.BusinessUseCases;
using MovieService.Controllers;
using MovieService.Domain.Actor;
using MovieService.Domain.Country;
using MovieService.Domain.Director;
using MovieService.Domain.Genre;
using MovieService.Domain.Movie;
using MovieService.Infrastructure;
using MovieService.Infrastructure.EF;
using TestHelper.Libraries;

namespace TestHelper
{
    public class DataInitialiser
    {
        #region Data Init
        List<Actor> _actorSource = new EditableList<Actor>()
        {
            new Actor()
            {
                ActorId = 1,
                ActorName = "Tom Holland"
            },
            new Actor()
            {
                ActorId = 2,
                ActorName = "Robert Downey Jr."
            },
            new Actor()
            {
                ActorId = 3,
                ActorName = "Marlon Brando"
            },
            new Actor()
            {
                ActorId = 5,
                ActorName = "Mark Ruffalo"
            },
        };
        List<Country> _countrySource = new EditableList<Country>()
        {
            new Country()
            {
                CountryId = 1,
                CountryName = "Europe"
            },
            new Country()
            {
                CountryId = 2,
                CountryName = "Asia"
            },
        };
        List<Director> _directorSource = new EditableList<Director>()
        {
            new Director()
            {
                DirectorId = 1,
                DirectorName = "Anthony Russo"
            },
            new Director()
            {
                DirectorId = 2,
                DirectorName = "Joe Russo"
            },
            new Director()
            {
                DirectorId = 3,
                DirectorName = "Francis Coppola"
            },
            new Director()
            {
                DirectorId = 4,
                DirectorName = "Bong Joon Ho"
            }
        };
        List<Genre> _genreSource = new EditableList<Genre>()
        {
            new Genre()
            {
                GenreId = 1,
                GenreName = "Action Movie"
            },
            new Genre()
            {
                GenreId = 2,
                GenreName = "Psychological Movie"
            },
        };
        //MovieId   | MovieName                         | ActorId               | CountryId | DirectorId            | GenreId
        //1         | Avengers Endgame(2019)            | 2(Robert Downey Jr.)  | 1(Europe) | 1(Anthony Russo)      | 1(Action Movie)
        //2         | Spider-Man: Far from Home (2019)  | 1(Tom Holland)        | 1(Europe) | 2(Joe Russo)          | 1(Action Movie)
        //3         | The Godfather 1 (1972)            | 3(Marlon Brando)      | 1(Europe) | 3(Francis Coppola)    | 1(Action Movie)
        //4         | Parasite (2019)                   | 4(Bong Joon Ho)       | 2(Asia)   | 4(Bong Joon Ho)       | 2(Psychological Movie)
        //5         | 
        List<Movie> _movieSource = new EditableList<Movie>()
        {
            new Movie()
            {
                MovieId = 1,
                MovieName = "Avengers Endgame (2019)",
                Title = "Avengers Endgame (2019)",
                Date = 2019,
                Time = 181,
                ActorId = 2,
                CountryId = 1,
                DirectorId = 1,
                GenreId = 1
            },
            new Movie()
            {
                MovieId = 2,
                MovieName = "Spider-Man: Far from Home (2019)",
                Title = "Spider-Man: Far from Home (2019)",
                Date = 2019,
                Time = 181,
                ActorId = 1,
                CountryId = 1,
                DirectorId = 2,
                GenreId = 1
            },
            new Movie()
            {
                MovieId = 3,
                MovieName = "The Godfather 1 (1972)",
                Title = "The Godfather 1 (1972)",
                Date = 1972,
                Time = 175,
                ActorId = 3,
                CountryId = 1,
                DirectorId = 3,
                GenreId = 1
            },
            new Movie()
            {
                MovieId = 4,
                MovieName = "Parasite (2019)",
                Title = "Parasite (2019)",
                Date = 2019,
                Time = 134,
                ActorId = 5,
                CountryId = 2,
                DirectorId = 4,
                GenreId = 2
            },
        };
        #endregion

        private MovieController _movieController;
        public MovieController InitMovieController
        {
            get
            {
                if(_movieController == null)
                {
                    var movieServiceMock = InitMovieService;
                    _movieController = new MovieController(movieServiceMock);
                }
                return _movieController;
            }
        }
        private MoviesService _movieService;
        public MoviesService InitMovieService
        {
            get
            {
                if(_movieService == null)
                {
                    var uniOfWorkMock = InitUnitOfWork;
                    _movieService = new MoviesService(uniOfWorkMock);
                }
                return _movieService;
            }
        }
        private UnitOfWork _unitOfWork;
        public UnitOfWork InitUnitOfWork
        {
            get
            {
                if (_unitOfWork == null)
                {
                    var movieMockContext = InitMovieMockContext;
                    _unitOfWork = new UnitOfWork(movieMockContext);
                }
                return _unitOfWork;
            }
        }
        private MovieContext _movieContext;
        public MovieContext InitMovieMockContext
        {
            get
            {
                if(_movieContext == null)
                {
                    foreach (var movie in _movieSource)
                    {
                        movie.Actor =
                            _actorSource.FirstOrDefault(a => a.ActorId == movie.ActorId);
                        movie.Country =
                            _countrySource.FirstOrDefault(v => v.CountryId == movie.CountryId);
                        movie.Director =
                            _directorSource.FirstOrDefault(e => e.DirectorId == movie.DirectorId);
                        movie.Genre =
                            _genreSource.FirstOrDefault(s => s.GenreId == movie.GenreId);
                    }

                    var contextOption = new DbContextOptions<MovieContext>();
                    var movieMockContext = new Mock<MovieContext>(contextOption);

                    movieMockContext.Setup(c => c.Movie).ReturnsDbSet(_movieSource);
                    movieMockContext.Setup(c => c.Actor).ReturnsDbSet(_actorSource);
                    movieMockContext.Setup(c => c.Country).ReturnsDbSet(_countrySource);
                    movieMockContext.Setup(c => c.Director).ReturnsDbSet(_directorSource);
                    movieMockContext.Setup(c => c.Genre).ReturnsDbSet(_genreSource);

                    _movieContext = movieMockContext.Object;
                }
                return _movieContext;
            }
        }
    }
}