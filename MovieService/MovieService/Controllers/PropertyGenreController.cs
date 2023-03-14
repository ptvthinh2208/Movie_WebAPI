using Microsoft.AspNetCore.Mvc;
using MovieService.Business.BusinessUseCases;
using MovieService.Business.DTOs;
using MovieService.Domain.Genre;

namespace MovieService.Controllers
{
    [Route("api/genre")]
    public class PropertyGenreController : BasePropertyController<Genre, GenreDto>
    {
        public PropertyGenreController(IPropertyService<Genre, GenreDto> propertyService)
        {
            SetService(propertyService);
        }
    }
}
