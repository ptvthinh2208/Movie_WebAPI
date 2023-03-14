using MovieService.Business.DTOs;
using MovieService.Domain.Genre;
using MovieService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieService.Business.BusinessUseCases
{
    public class PropertyGenreService : BasePropertyService<Genre, GenreDto>
    {

        public PropertyGenreService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        public override IPropertyRepository<Genre> GetPropertyRepository(IUnitOfWork unitOfWork)
        {
            return unitOfWork.GenreRepository;
        }
        public override Genre CreateEntity(string value)
        {
            return new Genre()
            {
                GenreName = value
            };
        }
        public override Genre UpdateEntity(Genre entity, string value)
        {
            entity.GenreName = value;
            return entity;
        }
        public override GenreDto MapToDto(Genre entity)
        {
            return new GenreDto()
            {
                GenreId = entity.GenreId,
                GenreName = entity.GenreName
            };
        }
    }
}
