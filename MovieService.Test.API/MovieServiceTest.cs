using Microsoft.AspNetCore.Mvc;
using MovieService.Business.DTOs;
using MovieService.Domain.Movie;
using TestHelper;

namespace MovieService.Test.API
{
    [TestFixture]
    public class MovieServiceTest
    {
        private DataInitialiser _dataInitialiser;
        [SetUp]
        public void Setup()
        {
            _dataInitialiser = new DataInitialiser();
        }

        [TearDown]
        public void TearDown()
        {
        }
        #region GET - api/movies - Get all movie

        [TestCase(200)]
        public async Task GetMovies(int resultStatusCode)
        {
            var movieController = _dataInitialiser.InitMovieController;

            //Act
            var result = await movieController.GetMovies();
            // Assert
            if (resultStatusCode == 200)
            {
                var okResult = result as OkObjectResult;
                if (okResult == null)
                {
                    Assert.Fail();
                }
                Assert.NotNull(okResult);
                Assert.AreEqual(resultStatusCode, okResult.StatusCode);

            }
            else if (resultStatusCode == 404)
            {
                var obj404Result = result as NotFoundResult;
                if (obj404Result == null)
                {
                    Assert.Fail();
                }
                Assert.AreEqual(resultStatusCode, obj404Result.StatusCode);
            }
        }
        #endregion

        #region POST - api\movies - Create a movie

        [TestCase(1, "Avengers Infinity War", "Avengers Infinity War", 2017, 123, "Robert Downey Jr.", "Europe", "Joe Russo", "Action Movie", 201)]
        [TestCase(2, "Sherlock Holmes", "Sherlock Holmes", 2009, 128, "Robert Downey Jr.", "Europe", "Guy Ritchie", "Action Movie", 400)]
        public async Task CreateMovie(int MovieId,string movieName, string title, int date, int time,
            string actorName, string countryName, string directorName, string genreName, 
            int resultStatusCode)
        {
            var requestMovie = new MovieDto()
            {
                MovieId = MovieId,
                MovieName = movieName,
                Title = title,
                Date = date,
                Time = time,
                ActorName = actorName,
                CountryName = countryName,
                DirectorName = directorName,
                GenreName = genreName,
            };

            var movieController = _dataInitialiser.InitMovieController;
            //Act
            var result = await movieController.CreateMovie(requestMovie);
            // Assert
            if (resultStatusCode == 201)
            {
                var createdResult = result as CreatedResult;
                if (createdResult == null)
                {
                    Assert.Fail();
                }
                Assert.AreEqual(resultStatusCode, createdResult.StatusCode);

                var createdMovieDto = createdResult.Value as MovieDto;
                if (createdMovieDto == null)
                {
                    Assert.Fail();
                }
                //Assert.AreEqual(createdFeatureDto.FeatureUrlId, expectFeatureUrlId);
                Assert.AreEqual(createdMovieDto.ActorName, requestMovie.ActorName);
                Assert.AreEqual(createdMovieDto.CountryName, requestMovie.CountryName);
                Assert.AreEqual(createdMovieDto.DirectorName, requestMovie.DirectorName);
                Assert.AreEqual(createdMovieDto.GenreName, requestMovie.GenreName);

            }
            else if (resultStatusCode == 400)
            {
                var badRequestResult = result as BadRequestObjectResult;
                if (badRequestResult == null)
                {
                    Assert.Fail();
                }
                Assert.AreEqual(resultStatusCode, badRequestResult.StatusCode);
            }
            else
            {
                Assert.Fail();
            }
        }
        #endregion

        #region GET - api\movies\{movieId} - Get movie by id

        [TestCase(null, "", 404)]
        [TestCase("1", "Avengers Endgame (2019)", 200)]
        [TestCase("4", "Parasite (2019)", 200)]
        [TestCase("-1", "", 404)]
        [TestCase("a", "", 404)]
        public async Task GetMovieById(string movieId, string movieName, int resultStatusCode)
        {
            var movieController = _dataInitialiser.InitMovieController;
            // Act
            var result = await movieController.GetMovies(movieId);
            // Assert
            if (result == null)
            {
                Assert.Fail();
            }

            if (resultStatusCode == 200)
            {
                var okResult = result as OkObjectResult;
                if (okResult == null)
                {
                    Assert.Fail();
                }
                Assert.AreEqual(resultStatusCode, okResult.StatusCode);
                var movie = okResult.Value as MovieDto;
                if (movie == null)
                {
                    Assert.Fail();
                }
                Assert.AreEqual(movieId, movie.MovieId.ToString());
                Assert.AreEqual(movieName, movie.MovieName);
            }
            else if (resultStatusCode == 404)
            {
                var obj404Result = result as NotFoundResult;
                if (obj404Result == null)
                {
                    Assert.Fail();
                }
                Assert.AreEqual(resultStatusCode, obj404Result.StatusCode);
            }
        }
        #endregion
        #region PUT - api\movies\{movieId} - Update movie by id

        [TestCase("0", "Test", "Test",2019,181, "Tom Holland", "Europe", "","Action",0, 204)]
        [TestCase("1", "King Kong", "King Kong", 2023, 181, "Tom Holland", "Europe", "Joe Russo", "Action Movie",1, 200)]
        [TestCase("3", "Sieu sao sieu ngo", "Sieu sao sieu ngo", 2019, 181, "Vinh Thinh", "Asia", "Joe Russo", "Action Movie",3, 204)]
        [TestCase("4", null, null, 2023, 180, "Tom Holland", "Europe","Joe Russo", "Psychological Movie",4, 200)]
        public async Task UpdateMovie(string MovieId, string movieName, string title, int date, int time,
            string actorName, string countryName, string directorName, string genreName,
            int exitsedId,int resultStatusCode)
        {
            
            var requestMovie = new MovieDto()
            {
                MovieId = exitsedId,
                MovieName = movieName,
                Title = title,
                Date = date,
                Time = time,
                ActorName = actorName,
                CountryName = countryName,
                DirectorName = directorName,
                GenreName = genreName,
            };
            var movieController = _dataInitialiser.InitMovieController;
            //Act
            var result = await movieController.UpdateMovieById(MovieId, requestMovie);

            if (resultStatusCode == 200)
            {
                var okObjResult = result as OkObjectResult;
                if (okObjResult == null)
                {
                    Assert.Fail();
                }
                Assert.AreEqual(resultStatusCode, okObjResult.StatusCode);

                var updatedMovie = okObjResult.Value as MovieDto;
                if (updatedMovie == null)
                {
                    Assert.Fail();
                }
                Assert.AreEqual(MovieId, updatedMovie.MovieId.ToString());

                if (requestMovie.ActorName != null)
                {
                    Assert.AreEqual(requestMovie.ActorName == "" ? null : requestMovie.ActorName, updatedMovie.ActorName);
                }
                if (requestMovie.CountryName != null)
                {
                    Assert.AreEqual(requestMovie.CountryName == "" ? null : requestMovie.CountryName, updatedMovie.CountryName);
                }
                if (requestMovie.DirectorName != null)
                {
                    Assert.AreEqual(requestMovie.DirectorName == "" ? null : requestMovie.DirectorName, updatedMovie.DirectorName);
                }
                if (requestMovie.GenreName != null)
                {
                    Assert.AreEqual(requestMovie.GenreName == "" ? null : requestMovie.GenreName, updatedMovie.GenreName);
                }
                
            }
            else if (resultStatusCode == 204)
            {
                var statusResult = result as ObjectResult;
                if (statusResult == null)
                {
                    Assert.Fail();
                }
                Assert.AreEqual(resultStatusCode, statusResult.StatusCode);
            }
            else
            {
                Assert.Fail();
            }
        }
        #endregion
    }
}