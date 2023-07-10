using ApiDtoLibrary.Users;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UniversityApi.API.Contracts;

namespace UniversityApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _repository;
        private readonly IMapper _mapper;

        public UsersController(IUsersRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApiUserDto>>> GetUsers()
        {
            var users = await _repository.GetAllUsersAsync();
            var output = _mapper.Map<IEnumerable<ApiUserDto>>(users);
            return Ok(output);
        }

        // GET: api/Users/someuserid9302
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiUserDto>> GetUserById(string id)
        {
            var user = await _repository.GetUserAsync(id);
            var output = _mapper.Map<ApiUserDto>(user);
            return Ok(output);
        }
    }
}
