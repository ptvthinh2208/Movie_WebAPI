using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieService.Domain
{
    public interface IPropertyRepository<T> where T : class
    {
        Task<T> GetByName(string name);
        Task<IEnumerable<T>> GetByNames(string[] names);
        Task<IEnumerable<T>> Get();
        Task<T> Get(string id);
        Task<T> Get(int id);
        Task Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
