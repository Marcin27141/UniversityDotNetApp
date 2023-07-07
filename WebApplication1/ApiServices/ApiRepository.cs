using AutoMapper;
using System.Net.Http;

namespace WebApplication1.ApiServices
{
    public abstract class ApiRepository
    {
        protected virtual string _apiPath { get; set; } = "https://localhost:7228/api";
        protected readonly HttpClient _httpClient = new HttpClient();
        protected readonly IMapper _mapper;

        public ApiRepository(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}
