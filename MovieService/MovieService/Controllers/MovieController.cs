using Microsoft.AspNetCore.Mvc;
using MovieService.Business.BusinessUseCases;
using MovieService.Business.DTOs;

namespace MovieService.Controllers
{
    [Produces("application/json")]
    [Route("api/movies")]
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;
        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<ActionResult> GetMovies()
        {
            try
            {
                var result = await _movieService.GetMovies();
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetMovies(string id)
        {
            try
            {

                var result = await _movieService.GetMovieById(id);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateMovie([FromBody] MovieDto recievedMovie)
        {
            try
            {
                var result = await _movieService.CreateMovie(recievedMovie);
                if (result.IsSuccess)
                {
                    return Created("", result.data);
                }
                else if (result.IsUpdatedSuccess)
                {
                    return Ok(result.data);
                }
                return BadRequest(result.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        [HttpPut]
        public async Task<ActionResult> UpdateMovieById(string movieId, [FromBody] MovieDto recievedMovie)
        {
            try
            {
                var result = await _movieService.UpdateMovieId(movieId, recievedMovie);
                if (result.IsSuccess)
                {
                    return Ok(result.data);
                }
                return StatusCode(204, result.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
