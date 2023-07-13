using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebApplication1.ApiServices.BaseRepositories;
using WebApplication1.Contracts;
using WebApplication1.Services;

namespace WebApplication1.ApiServices.GenericRepositories
{
    public abstract class GenericPutRepository<T, U> : ApiRepository, IGenericPutRepository<T>
        where T : IDistinguishableEntity
        where U : class
    {
        protected GenericPutRepository(IMapper mapper) : base(mapper)
        {
        }

        public async Task<Guid> UpdateAsync(T updatedEntity)
        {
            var postEntity = _mapper.Map<U>(updatedEntity);
            var serializedContent = GetSerializedContent(postEntity);
            string updatePath = GetPathForUpdate(updatedEntity.EntityId);
            var response = await _httpClient.PutAsync(updatePath, serializedContent);
            if (response.IsSuccessStatusCode)
                return updatedEntity.EntityId;
            return default;
        }

        protected virtual string GetPathForUpdate(Guid entityId) => $"{_apiPath}/{entityId}";
    }
}