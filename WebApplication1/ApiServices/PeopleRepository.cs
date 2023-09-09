using ApiDtoLibrary.Person;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.ApiServices.BaseRepositories;
using WebApplication1.Contracts;
using WebApplication1.Services.People;

namespace WebApplication1.ApiServices
{
    public class PeopleRepository : ApiRepository, IPeopleRepository
    {
        private readonly IAuthenticationRepository _authenticationRepository;

        public PeopleRepository(IMapper mapper, IAuthenticationRepository authenticationRepository) : base(mapper)
        {
            _apiPath += "/People";
            _authenticationRepository = authenticationRepository;
        }

        public List<Person> GetAllPersonalData()
        {
            var response = _httpClient.GetAsync(_apiPath).Result;
            if (response.IsSuccessStatusCode)
            {
                var getResult = response.Content.ReadFromJsonAsync<List<GetPersonDto>>().Result;
                var result = _mapper.Map<List<Person>>(getResult);
                return result;
            }
            return default;
        }

        public async Task DeleteAsync(Guid id)
        {
            string deletePath = $"{_apiPath}/{id}";
            await _httpClient.DeleteAsync(deletePath);
            //await _authenticationRepository.RemoveClaimAsync(person.ApplicationUserId, "EntityPersonId");
        }

        public async Task<Person> GetPerson(Guid id)
        {
            string getPath = $"{_apiPath}/{id}";
            var response = await _httpClient.GetAsync(getPath);
            if (response.IsSuccessStatusCode)
            {
                var getResult = await response.Content.ReadFromJsonAsync<GetPersonDto>();
                var result = _mapper.Map<Person>(getResult);
                return result;
            }
            return default;
        }
    }
}
