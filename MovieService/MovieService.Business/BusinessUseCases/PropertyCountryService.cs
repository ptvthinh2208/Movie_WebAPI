using MovieService.Business.DTOs;
using MovieService.Domain.Country;
using MovieService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieService.Business.BusinessUseCases
{
    public class PropertyCountryService : BasePropertyService<Country, CountryDto>
    {
        public PropertyCountryService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        public override IPropertyRepository<Country> GetPropertyRepository(IUnitOfWork unitOfWork)
        {
            return unitOfWork.CountryRepository;
        }
        public override Country CreateEntity(string value)
        {
            return new Country()
            {
                CountryName = value
            };
        }

        public override Country UpdateEntity(Country entity, string value)
        {
            entity.CountryName = value;
            return entity;
        }

        public override CountryDto MapToDto(Country entity)
        {
            return new CountryDto()
            {
                CountryId = entity.CountryId,
                CountryName = entity.CountryName
            };
        }
    }
}
