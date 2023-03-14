using Microsoft.AspNetCore.Mvc;
using MovieService.Business.BusinessUseCases;
using MovieService.Business.DTOs;
using MovieService.Domain.Country;

namespace MovieService.Controllers
{
    [Produces("application/json")]
    [Route("api/country")]
    public class PropertyCountryController : BasePropertyController<Country, CountryDto>
    {
        public PropertyCountryController(IPropertyService<Country, CountryDto> countryService) : base()
        {
            SetService(countryService);
        }
    }
}
