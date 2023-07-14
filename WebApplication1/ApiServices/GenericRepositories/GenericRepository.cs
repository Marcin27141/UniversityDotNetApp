using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.ApiServices.BaseRepositories;
using WebApplication1.Contracts;
using WebApplication1.Services;

namespace WebApplication1.ApiServices.GenericRepositories
{
    public abstract class GenericRepository<T, U> : ApiRepository, IGenericRepository<T,U>
        where T : IDistinguishableEntity
    {
        private readonly IGenericGetRepository<T> _getRepository;
        private readonly IGenericPostRepository<T, U> _postRepository;
        private readonly IGenericPutRepository<T> _putRepository;

        protected GenericRepository(IMapper mapper,
            IGenericGetRepository<T> getRepository,
            IGenericPostRepository<T, U> postRepository,
            IGenericPutRepository<T> putRepository) : base(mapper)
        {
            _getRepository = getRepository;
            _postRepository = postRepository;
            _putRepository = putRepository;
        }

        public Task<U> AddAsync(T entity) => _postRepository.AddAsync(entity);

        public async Task DeleteAsync(Guid id)
        {
            string deletePath = GetPathForDelete(id);
            await _httpClient.DeleteAsync(deletePath);
        }

        protected virtual string GetPathForDelete(object entityId) => _apiPath;

        public Task<List<T>> GetAllAsync() => _getRepository.GetAllAsync();
        public Task<T> GetAsync(Guid id) => _getRepository.GetAsync(id);
        public Task<T> GetByUserAsync(string userId) => _getRepository.GetByUserAsync(userId);

        public Task<Guid> UpdateAsync(T updatedEntity) => _putRepository.UpdateAsync(updatedEntity);
    }
}
