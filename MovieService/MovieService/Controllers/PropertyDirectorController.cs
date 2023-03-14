using Microsoft.AspNetCore.Mvc;
using MovieService.Business.BusinessUseCases;
using MovieService.Business.DTOs;
using MovieService.Domain.Director;

namespace MovieService.Controllers
{
    [Produces("application/json")]
    [Route("api/director")]
    public class PropertyDirectorController : BasePropertyController<Director, DirectorDto>
    {
        public PropertyDirectorController(IPropertyService<Director, DirectorDto> directorService) : base()
        {
            SetService(directorService);
        }
    }
}
