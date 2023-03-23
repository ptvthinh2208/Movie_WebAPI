using MovieService.Domain.Genre;
using MovieService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieService.Business.Common;

namespace MovieService.Business.BusinessUseCases
{
    public abstract class BasePropertyService<T, TDto> : IPropertyService<T, TDto> where T : class where TDto : class
    {
        private readonly IPropertyRepository<T> _propertyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public abstract IPropertyRepository<T> GetPropertyRepository(IUnitOfWork unitOfWork);

        protected BasePropertyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _propertyRepository = GetPropertyRepository(unitOfWork);
        }

        public async Task<IEnumerable<TDto>> Get()
        {
            var entityEnum = await _propertyRepository.Get();
            var entities = entityEnum as T[];
            if (entities == null)
            {
                return new TDto[0];
            }

            List<TDto> entityDtos = new List<TDto>();
            foreach (var entity in entities)
            {
                entityDtos.Add(MapToDto(entity));
            }
            return entityDtos;
        }

        public async Task<TDto> GetById(string strId)
        {
            T entity = null;
            
            if (Int32.TryParse(strId, out var id))
            {
                entity = await _propertyRepository.GetById(id);
            }

            return entity == null ? null : MapToDto(entity);
        }

        public async Task<ReturnResult> Create(string value)
        {
            try
            {
                var existEntity = await _propertyRepository.GetByName(value);
                if (existEntity != null)
                {
                    return new ReturnResult()
                    {
                        IsSuccess = false,
                        Message = "Duplicate " + typeof(T).Name
                    };
                }
                T entity = CreateEntity(value);
                await _propertyRepository.Create(entity);
                _unitOfWork.Save();
                return new ReturnResult()
                {
                    IsSuccess = true,
                    data = MapToDto(entity)
                };
            }
            catch (Exception ex)
            {
                return new ReturnResult()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ReturnResult> Update(int id, string value)
        {
            try
            {
                var existEntity = await _propertyRepository.GetById(id);
                if (existEntity == null)
                {
                    return new ReturnResult()
                    {
                        IsSuccess = false,
                        Message = typeof(T).Name + " doesn't exist"
                    };
                }
                else
                {
                    var existValue = await _propertyRepository.GetByName(value);
                    if (existValue != null)
                    {
                        return new ReturnResult()
                        {
                            IsSuccess = false,
                            Message = "Can't update, because " + existValue + "alreay exitst!"
                        };
                    }
                }
                T entity = UpdateEntity(existEntity, value);
                _propertyRepository.Update(entity);
                _unitOfWork.Save();
                return new ReturnResult()
                {
                    IsSuccess = true,
                    data = MapToDto(entity)
                };
            }
            catch (Exception ex)
            {
                return new ReturnResult()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<ReturnResult> Delete(string strId)
        {
            try
            {
                T existEntity = null;

                if (Int32.TryParse(strId, out var id))
                {
                    existEntity = await _propertyRepository.GetById(id);
                }
                if (existEntity == null)
                {
                    return new ReturnResult()
                    {
                        IsSuccess = false,
                        Message = "Value doesn't exist"
                    };
                }
                _propertyRepository.Delete(existEntity);
                _unitOfWork.Save();
                return new ReturnResult()
                {
                    IsSuccess = true
                };
            }
            catch (Exception ex)
            {
                return new ReturnResult()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public virtual T CreateEntity(string value)
        {
            throw new NotImplementedException();
        }

        public virtual T UpdateEntity(T entity, string value)
        {
            throw new NotImplementedException();
        }

        public virtual TDto MapToDto(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
