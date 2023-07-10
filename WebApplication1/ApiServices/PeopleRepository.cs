using ApiDtoLibrary.Person;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
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

        private IEnumerable<Person> GetPersonalDataFromApi()
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

        private PersonType GetPersonType(Person person)
        {
            switch (person.EntityClassId)
            {
                case 2:
                    return PersonType.Professor;
                case 3:
                    return PersonType.Student;
                default:
                    throw new Exception("Unrecognized entity class id");
            }
        }

        public List<KeyTypePersonalData> GetAllPersonalData()
        {
            var people = GetPersonalDataFromApi();
            return people
                .Select(p => new KeyTypePersonalData
                {
                    Id = p.EntityPersonID,
                    Type = GetPersonType(p),
                    PersonalData = p.PersonalData,
                }).ToList();
        }
    }
}
