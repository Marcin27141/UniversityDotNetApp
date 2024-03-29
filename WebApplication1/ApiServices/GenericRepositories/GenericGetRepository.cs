﻿using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
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
    public abstract class GenericGetRepository<T, U> : ApiRepository, IGenericGetRepository<T>
        where T : IDistinguishableEntity
        where U : class
    {
        private readonly IMemoryCache _memoryCache;
        private const int CACHE_SECONDS_TIME = 120;

        protected GenericGetRepository(IMapper mapper, IMemoryCache memoryCache) : base(mapper)
        {
            this._memoryCache = memoryCache;
        }
        protected virtual string GetPathForGet(Guid id) => $"{_apiPath}/{id}";
        protected virtual string GetPathForGetByUser(string id) => $"{_apiPath}/User/{id}";
        protected virtual string GetPathForGetAll() => _apiPath;

        public async Task<T> GetAsync(Guid id)
        {
            string getByIdPath = GetPathForGet(id);
            var response = await _httpClient.GetAsync(getByIdPath);
            if (response.IsSuccessStatusCode)
            {
                var getResult = await response.Content.ReadFromJsonAsync<U>();
                var result = _mapper.Map<T>(getResult);
                return result;
            }
            return default;
        }

        private async Task<List<T>> GetAllFromApiAsync()
        {
            string byAllPath = GetPathForGetAll();
            var response = await _httpClient.GetAsync(byAllPath);
            if (response.IsSuccessStatusCode)
            {
                var getResults = await response.Content.ReadFromJsonAsync<List<U>>();
                var result = _mapper.Map<List<T>>(getResults);
                return result;
            }
            return Enumerable.Empty<T>().ToList();
        }

        protected abstract string GetCacheKeyForGetAll();

        public Task<List<T>> GetAllAsync()
        {
            return _memoryCache.GetOrCreateAsync(
                GetCacheKeyForGetAll(),
                cacheEntry =>
                {
                    cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(CACHE_SECONDS_TIME);
                    return GetAllFromApiAsync();
                });
        }

        public async virtual Task<T> GetByUserAsync(string userId)
        {
            string getByUserPath = GetPathForGetByUser(userId);
            var response = await _httpClient.GetAsync(getByUserPath);
            if (response.IsSuccessStatusCode)
            {
                var getResult = await response.Content.ReadFromJsonAsync<U>();
                var result = _mapper.Map<T>(getResult);
                return result;
            }
            return default;
        }
    }
}