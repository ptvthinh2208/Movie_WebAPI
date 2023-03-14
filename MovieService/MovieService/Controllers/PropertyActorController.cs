using Microsoft.AspNetCore.Mvc;
using MovieService.Business.BusinessUseCases;
using MovieService.Business.DTOs;
using MovieService.Domain.Actor;

namespace MovieService.Controllers
{
    [Produces("application/json")]
    [Route("api/actor")]
    public class PropertyActorController : BasePropertyController<Actor, ActorDto>
    {
        public PropertyActorController(IPropertyService<Actor, ActorDto> actorService) : base()
        {
            SetService(actorService);
        }
    }
}
