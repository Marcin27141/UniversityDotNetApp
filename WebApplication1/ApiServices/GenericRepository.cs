using ApiDtoLibrary;
using ApiDtoLibrary.Professors;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Contracts;
using WebApplication1.Services;
using WebApplication1.Services.People;

namespace WebApplication1.ApiServices
{
    public abstract class GenericRepository<T, U> : ApiRepository, IGenericRepository<T>
        where T : IDistinguishableEntity
        where U : class
    {
        protected GenericRepository(IMapper mapper) : base(mapper)
        {
        }

        protected virtual string GetPathForGetById(int id) => $"{_apiPath}/{id}";
        protected virtual string GetPathForGetByUser(string id) => $"{_apiPath}/ByUser/{id}";
        protected virtual string GetPathForGetAll() => _apiPath;
        protected virtual string GetPathForCreate() => _apiPath;
        protected virtual string GetPathForUpdate(int id) => $"{_apiPath}/{id}";
        protected virtual string GetPathForDeleteById(int id) => $"{_apiPath}/{id}";

        public T GetById(int id)
        {
            string getByIdPath = GetPathForGetById(id);
            var response = _httpClient.GetAsync(getByIdPath).Result;
            if (response.IsSuccessStatusCode)
            {
                var getResult = response.Content.ReadFromJsonAsync<U>().Result;
                var result = _mapper.Map<T>(getResult);
                return result;
            }
            return default;
        }

        public List<T> GetAll()
        {
            string byAllPath = GetPathForGetAll();
            var response = _httpClient.GetAsync(byAllPath).Result;
            if (response.IsSuccessStatusCode)
            {
                var getResults = response.Content.ReadFromJsonAsync<List<U>>().Result;
                var result = _mapper.Map<List<T>>(getResults);
                return result;
            }
            return Enumerable.Empty<T>().ToList();
        }

        public virtual T GetByUser(string userId)
        {
            string getByUserPath = GetPathForGetByUser(userId);
            var response = _httpClient.GetAsync(getByUserPath).Result;
            if (response.IsSuccessStatusCode)
            {
                var getResult = response.Content.ReadFromJsonAsync<U>().Result;
                var result = _mapper.Map<T>(getResult);
                return result;
            }
            return default;
        }

        public async Task<string> AddAsync(T entity)
        {
            var postEntity = _mapper.Map<U>(entity);
            var serializedContent = GetSerializedContent(postEntity);
            string createPath = GetPathForCreate();
            var response = await _httpClient.PostAsync(createPath, serializedContent);
            if (response.IsSuccessStatusCode)
                return entity.Key;
            return null;
        }

        public async Task<string> UpdateAsync(int id, T updatedEntity)
        {
            var postEntity = _mapper.Map<U>(updatedEntity);
            var serializedContent = GetSerializedContent(postEntity);
            string updatePath = GetPathForUpdate(updatedEntity.EntityId);
            var response = await _httpClient.PutAsync(updatePath, serializedContent);
            if (response.IsSuccessStatusCode)
                return updatedEntity.Key;
            return null;
        }

        public Task DeleteByIdAsync(int id)
        {
            string deleteByIdPath = GetPathForDeleteById(id);
            return _httpClient.DeleteAsync(deleteByIdPath);
        }

        protected HttpContent GetSerializedContent(U postEntity)
        {
            return new StringContent(JsonConvert.SerializeObject(postEntity), Encoding.UTF8, "application/json");
        }
    }
}
