using ApiDtoLibrary.Person;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebApplication1.ApiServices.BaseRepositories;
using WebApplication1.Contracts;
using WebApplication1.Services.People;

namespace WebApplication1.ApiServices
{
    public class PeopleRepository : ApiRepository, IPeopleRepository
    {
        public PeopleRepository(IMapper mapper) : base(mapper)
        {
            _apiPath += "/People";
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
        }
    }
}
