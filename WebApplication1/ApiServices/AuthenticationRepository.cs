using ApiDtoLibrary.Authentication;
using ApiDtoLibrary.Users;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using WebApplication1.Contracts;
using WebApplication1.Converters;
using WebApplication1.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApplication1.ApiServices
{
    public class AuthenticationRepository : ApiRepository, IAuthenticationRepository
    {
        public AuthenticationRepository(IMapper mapper) : base(mapper)
        {
            _apiPath += "/Authentication";
        }

        public async Task<IdentityResult> AddClaimAsync(ApplicationUser user, Claim claim)
        {
            string path = $"{_apiPath}/Claims/{user.Id}";
            var serializedClaim = GetSerializedContent(claim);
            var response = await _httpClient.PostAsync(path, serializedClaim);
            //var result = await response.Content.ReadFromJsonAsync<IdentityResult>(options);
            var jsonContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<IdentityResult>(jsonContent, new IdentityResultConverter());
            return result;
        }

        public bool ConfirmedAccountRequired()
        {
            string path = $"{_apiPath}/ConfirmedAccountRequired";
            var response = _httpClient.GetAsync(path).Result;
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                return bool.Parse(result);
            }
            return default;
        }

        public async Task<IdentityResult> CreateUserAsync(ApplicationUser user)
        {
            string path = $"{_apiPath}/Create";
            var mappedUser = _mapper.Map<ApiUserDto>(user);
            var serializedUser = GetSerializedContent(mappedUser);
            var response = await _httpClient.PostAsync(path, serializedUser);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                var errorResponse = JObject.Parse(errorContent);
                var errors = errorResponse["errors"];

                if (errors != null)
                {
                    //var errorList = errors.ToObject<Dictionary<string, List<string>>>();

                    //var identityErrors = errorList.SelectMany(kv => kv.Value.Select(errorMsg => new IdentityError
                    //{
                    //    Code = kv.Key,
                    //    Description = errorMsg
                    //})).ToList();

                    var identityErrors = errors.Select(error => new IdentityError
                    {
                        Code = error["code"]?.ToString(),
                        Description = error["description"]?.ToString()
                    }).ToList();

                    return IdentityResult.Failed(identityErrors.ToArray());
                }
            }

            return IdentityResult.Success;
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(ApplicationUser user)
        {
            string path = $"{_apiPath}/EmailConfirmationToken/{user.Id}";
            var response = await _httpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return result;
            }
            return default;
        }

        public async Task<IEnumerable<AuthenticationScheme>> GetExternalAuthenticationSchemesAsync()
        {
            string path = $"{_apiPath}/ExternalSchemes";
            var response = await _httpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<IEnumerable<AuthenticationScheme>>();
                return result;
            }
            return default;
        }

        public async Task<string> GetIdByUsernameAsync(string username)
        {
            string path = $"{_apiPath}/UserId/{username}";
            var response = await _httpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return result;
            }
            return default;
        }

        public async Task<SignInResult> PasswordSignInAsync(ApplicationUser user, bool rememberMe, bool lockoutOnFailure)
        {
            string path = $"{_apiPath}/PasswordSignIn/rememberMe={rememberMe}/lockoutOnFailure={lockoutOnFailure}";
            var mappedUser = _mapper.Map<LoginDto>(user);
            var serializedUser = GetSerializedContent(mappedUser);
            var response = await _httpClient.PostAsync(path, serializedUser);
            var dto = await response.Content.ReadFromJsonAsync<SignInResultDto>();
            return new ManualSignInResult(dto);
        }

        public async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            string path = $"{_apiPath}/SignIn/{user.Id}/isPersistent={isPersistent}";
            await _httpClient.GetAsync(path);
        }
    }

    class ManualSignInResult : SignInResult
    {
        public ManualSignInResult(SignInResultDto dto)
        {
            Succeeded = dto.Succeeded;
            RequiresTwoFactor = dto.RequiresTwoFactor;
            IsLockedOut = dto.IsLockedOut;
        }
    }
}
