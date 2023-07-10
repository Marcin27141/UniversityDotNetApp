using AutoMapper;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

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

        protected HttpContent GetSerializedContent(object postEntity)
        {
            return new StringContent(JsonConvert.SerializeObject(postEntity), Encoding.UTF8, "application/json");
        }
    }
}
