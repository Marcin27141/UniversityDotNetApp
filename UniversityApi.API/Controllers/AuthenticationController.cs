using ApiDtoLibrary.Authentication;
using ApiDtoLibrary.Users;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using System.Security.Claims;
using System.Text.Json;
using UniversityApi.API.Contracts;
using UniversityApi.API.DataBase.Identity;
using UniversityApi.JsonConverters;

namespace UniversityApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAuthenticationRepository _authenticationRepository;

        public AuthenticationController(IMapper mapper, IAuthenticationRepository authenticationRepository)
        {
            _mapper = mapper;
            _authenticationRepository = authenticationRepository;
        }

        // GET: api/Authentication/SignIn
        [HttpGet]
        [Route("SignIn/{userId}/isPersistent={isPersistent}")]
        public async Task<ActionResult> SignInAsync(string userId, bool isPersistent)
        {
            await _authenticationRepository.SignInAsync(userId, isPersistent);
            return Ok();
        }

        // POST: api/Authentication/PasswordSignIn
        [HttpPost]
        [Route("PasswordSignIn/rememberMe={rememberMe}/lockoutOnFailure={lockoutOnFailure}")]
        //[Route("PasswordSignIn")]
        public async Task<ActionResult> PasswordSignInAsync([FromBody] LoginDto loginDto, bool rememberMe, bool lockoutOnFailure)
        {
            var result = await _authenticationRepository.PasswordSignInAsync(loginDto, rememberMe, lockoutOnFailure);
            if (result.Succeeded)
            {
                var dto = _mapper.Map<SignInResultDto>(result);
                return Ok(dto);
            }
            return BadRequest(result);
        }

        // GET: api/Authentication/ExternalSchemes
        [HttpGet]
        [Route("ExternalSchemes")]
        public async Task<ActionResult> GetExternalSchemesAsync()
        {
            var result = await _authenticationRepository.GetExternalAuthenticationSchemesAsync();
            return Ok(result);
        }

        // GET: api/Authentication/EmailConfirmationToken
        [HttpGet]
        [Route("EmailConfirmationToken/{userId}")]
        public async Task<ActionResult> GetEmailConfirmationTokenAsync(string userId)
        {
            var result = await _authenticationRepository.GenerateEmailConfirmationTokenAsync(userId);
            return Ok(result);
        }

        // POST: api/Authentication/Create
        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult> CreateUserAsync(ApiUserDto userDto)
        {
            var user = _mapper.Map<ApiUser>(userDto);
            var result = await _authenticationRepository.CreateUserAsync(user, userDto.Password);
            if (result.Succeeded)
            {
                return Ok(user.Id);
            }
            return BadRequest(result);
        }

        // GET: api/Authentication/ConfirmedAccountRequired
        [HttpGet]
        [Route("ConfirmedAccountRequired")]
        public ActionResult CheckIfConfirmedAccountRequired()
        {
            var result = _authenticationRepository.ConfirmedAccountRequired();
            return Ok(result);
        }

        // GET: api/Authentication/UserId
        [HttpGet]
        [Route("UserId/{username}")]
        public async Task<ActionResult> GetUserIdByUsernameAsync(string username)
        {
            var result = await _authenticationRepository.GetUserIdByUsernameAsync(username);
            return result != null ? Ok(result) : NotFound(result);
        }

        // POST: api/Authentication/Claims/someUsername
        [HttpPost]
        [Route("Claims/{userId}")]
        public async Task<ActionResult> AddClaimAsync(string userId, [FromBody] JsonElement claimJson)
        {
            var options = new JsonSerializerOptions();
            options.Converters.Add(new ClaimConverter());
            var claim = JsonSerializer.Deserialize<Claim>(claimJson.GetRawText(), options);

            var result = await _authenticationRepository.AddClaimAsync(userId, claim);
            if (result.Succeeded)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
