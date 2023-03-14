using MovieService.Business.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieService.Business.BusinessUseCases
{
    public interface IPropertyService<T, TDto> where T : class where TDto : class
    {
        Task<IEnumerable<TDto>> Get();
        Task<TDto> GetById(string id);
        Task<ReturnResult> Create(string value);
        Task<ReturnResult> Update(int id, string value);
        Task<ReturnResult> Delete(string id);
    }
}
