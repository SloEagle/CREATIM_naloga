using System.Net.Http.Json;

namespace CREATIM_naloga.Client.Services.SmsService
{
    public class SmsService : ISmsService
    {
        private readonly HttpClient _http;

        public SmsService(HttpClient http)
        {
            _http = http;
        }

        public Provider Provider { get; set; }

        public async Task GetProvider()
        {
            var response = await _http.GetFromJsonAsync<Provider>("api/sms");
            if(response != null )
            {
                Provider = response;
            }
        }
    }
}
