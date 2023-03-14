using MovieService.Business.DTOs;
using MovieService.Domain.Director;
using MovieService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieService.Business.BusinessUseCases
{
    public class PropertyDirectorService : BasePropertyService<Director, DirectorDto>
    {
        public PropertyDirectorService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        public override IPropertyRepository<Director> GetPropertyRepository(IUnitOfWork unitOfWork)
        {
            return unitOfWork.DirectorRepository;
        }
        public override Director CreateEntity(string value)
        {
            return new Director()
            {
                DirectorName = value
            };
        }

        public override Director UpdateEntity(Director entity, string value)
        {
            entity.DirectorName = value;
            return entity;
        }

        public override DirectorDto MapToDto(Director entity)
        {
            return new DirectorDto()
            {
                DirectorId = entity.DirectorId,
                DirectorName = entity.DirectorName
            };
        }
    }
}
