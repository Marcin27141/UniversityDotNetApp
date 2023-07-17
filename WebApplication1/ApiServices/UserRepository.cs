using ApiDtoLibrary.Users;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.ApiServices.BaseRepositories;
using WebApplication1.Contracts;
using WebApplication1.Services;
using WebApplication1.Services.People;

namespace WebApplication1.ApiServices
{
    public class UserRepository : ApiRepository, IUserRepository
    {
        public UserRepository(IMapper mapper) : base(mapper)
        {
            _apiPath += "/Users";
        }

        public async Task<List<ApplicationUser>> GetUnsetNonadminUsersAsync()
        {
            var response = await _httpClient.GetAsync(_apiPath);
            if (response.IsSuccessStatusCode)
            {
                var getResults = await response.Content.ReadFromJsonAsync<List<ApiUserDto>>();
                var result = _mapper.Map<List<ApplicationUser>>(getResults);
                return result;
            }
            return Enumerable.Empty<ApplicationUser>().ToList();
        }

        public async Task<ApplicationUser> GetUserAsync(string id)
        {
            string getByIdPath = $"{_apiPath}/{id}";
            var response = await _httpClient.GetAsync(getByIdPath);
            if (response.IsSuccessStatusCode)
            {
                var getResult = await response.Content.ReadFromJsonAsync<ApiUserDto>();
                var result = _mapper.Map<ApplicationUser>(getResult);
                return result;
            }
            return default;
        }

        public bool IsSignedIn(ClaimsPrincipal user)
        {
            var userId = user.FindFirst(ClaimTypes.NameIdentifier);
            if (userId == null) return false;
            string signInCheckPath = $"{_apiPath}/SignInCheck/{userId}";
            var response = _httpClient.GetAsync(signInCheckPath).Result;
            if (response.IsSuccessStatusCode)
            {
                var getResult = response.Content.ReadAsStringAsync().Result;
                return bool.Parse(getResult);
            }
            return false;
        }
    }
}
