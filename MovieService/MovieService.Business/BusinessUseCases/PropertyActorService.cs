using MovieService.Business.DTOs;
using MovieService.Domain.Actor;
using MovieService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieService.Business.BusinessUseCases
{
    public class PropertyActorService : BasePropertyService<Actor, ActorDto>
    {
        public PropertyActorService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public override IPropertyRepository<Actor> GetPropertyRepository(IUnitOfWork unitOfWork)
        {
            return unitOfWork.ActorRepository;
        }

        public override Actor CreateEntity(string value)
        {
            return new Actor()
            {
                ActorName = value
            };
        }

        public override Actor UpdateEntity(Actor entity, string value)
        {
            entity.ActorName = value;
            return entity;
        }

        public override ActorDto MapToDto(Actor entity)
        {
            return new ActorDto()
            {
                ActorId = entity.ActorId,
                ActorName = entity.ActorName
            };
        }
    }
}
