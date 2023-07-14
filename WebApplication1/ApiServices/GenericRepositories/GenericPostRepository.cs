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
    public abstract class GenericPostRepository<T, U, V> : ApiRepository, IGenericPostRepository<T, V>
        where T : IDistinguishableEntity
        where U : class
    {
        protected GenericPostRepository(IMapper mapper) : base(mapper)
        {
        }

        public async Task<V> AddAsync(T entity)
        {
            var postEntity = _mapper.Map<U>(entity);
            var serializedContent = GetSerializedContent(postEntity);
            string createPath = GetPathForCreate();
            var response = await _httpClient.PostAsync(createPath, serializedContent);
            if (response.IsSuccessStatusCode)
            {
                var postResult = await response.Content.ReadFromJsonAsync<V>();
                return postResult;
            }
            return default;
        }

        protected virtual string GetPathForCreate() => _apiPath;
    }
}