using AutoMapper;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace WebApplication1.ApiServices.BaseRepositories
{
    public abstract class ApiRepository
    {
        protected string _apiPath { get; set; } = "https://localhost:7778/api";
        protected readonly HttpClient _httpClient = new HttpClient();
        protected readonly IMapper _mapper;

        public ApiRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        protected HttpContent GetSerializedContent(object postEntity)
        {
            var serialized = JsonConvert.SerializeObject(postEntity);
            return new StringContent(serialized, Encoding.UTF8, "application/json");
        }
    }
}
