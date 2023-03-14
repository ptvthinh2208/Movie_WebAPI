using Microsoft.AspNetCore.Mvc;
using MovieService.Business.BusinessUseCases;

namespace MovieService.Controllers
{
    [Produces("application/json")]
    public abstract class BasePropertyController<T, TDto> : Controller where T : class where TDto : class
    {
        private IPropertyService<T, TDto> _propertyService;
        protected void SetService(IPropertyService<T, TDto> propertyService)
        {
            _propertyService = propertyService;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                return Ok(await _propertyService.Get());
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            try
            {
                var result = await _propertyService.GetById(id);
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
        public async Task<ActionResult> Post([FromBody] string value)
        {
            try
            {
                var result = await _propertyService.Create(value);
                if (result.IsSuccess)
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

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] string value)
        {
            try
            {
                var result = await _propertyService.Update(id, value);
                if (result.IsSuccess)
                {
                    return Ok(result.data);
                }
                return NotFound(result.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                var result = await _propertyService.Delete(id);
                if (result.IsSuccess)
                {
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
