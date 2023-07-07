using ApiDtoLibrary.Users;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using WebApplication1.Contracts;
using WebApplication1.Services;

namespace WebApplication1.ApiServices
{
    public class UserRepository : ApiRepository, IUserRepository
    {
        public UserRepository(IMapper mapper) : base(mapper)
        {
            _apiPath += "/Users";
        }

        public List<ApplicationUser> GetAllUsers()
        {
            var response = _httpClient.GetAsync(_apiPath).Result;
            if (response.IsSuccessStatusCode)
            {
                var getResults = response.Content.ReadFromJsonAsync<List<ApiUserDto>>().Result;
                var result = _mapper.Map<List<ApplicationUser>>(getResults);
                return result;
            }
            return Enumerable.Empty<ApplicationUser>().ToList();
        }

        public ApplicationUser GetUserById(string id)
        {
            string getByIdPath = $"{_apiPath}/{id}";
            var response = _httpClient.GetAsync(getByIdPath).Result;
            if (response.IsSuccessStatusCode)
            {
                var getResult = response.Content.ReadFromJsonAsync<ApiUserDto>().Result;
                var result = _mapper.Map<ApplicationUser>(getResult);
                return result;
            }
            return default;
        }
    }
}
