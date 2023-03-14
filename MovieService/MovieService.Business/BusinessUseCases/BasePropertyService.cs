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
            if (typeof(T) == typeof(Genre))
            {
                entity = await _propertyRepository.Get(strId);
            }
            else if (Int32.TryParse(strId, out var id))
            {
                entity = await _propertyRepository.Get(id);
            }

            return entity == null ? null : MapToDto(entity);
        }

        public async Task<ReturnResult> Create(string value)
        {
            try
            {
                if (typeof(T) == typeof(Genre))
                {
                    var existSysId = await _propertyRepository.Get(value);
                    if (existSysId != null)
                    {
                        return new ReturnResult()
                        {
                            IsSuccess = false,
                            Message = "Duplicate SysId"
                        };
                    }
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
                if (typeof(T) == typeof(Genre))
                {
                    return new ReturnResult()
                    {
                        IsSuccess = false,
                        Message = "Can't update Genre"
                    };
                }
                var existEntity = await _propertyRepository.Get(id);
                if (existEntity == null)
                {
                    return new ReturnResult()
                    {
                        IsSuccess = false,
                        Message = typeof(T).Name + " doesn't exist"
                    };
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
                if (typeof(T) == typeof(Genre))
                {
                    existEntity = await _propertyRepository.Get(strId);
                }
                else
                {
                    if (Int32.TryParse(strId, out var id))
                    {
                        existEntity = await _propertyRepository.Get(id);
                    }
                }
                if (existEntity == null)
                {
                    return new ReturnResult()
                    {
                        IsSuccess = false
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
