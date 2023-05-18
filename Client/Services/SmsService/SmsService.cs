using System.Net.Http.Json;

namespace CREATIM_naloga.Client.Services.SmsService
{
    public class SmsService : ISmsService
    {
        private readonly HttpClient _http;
        private readonly ILogger<SmsService> _logger;

        public SmsService(HttpClient http, ILogger<SmsService> logger)
        {
            _http = http;
            _logger = logger;
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

        public async Task SendGroupSMS(int groupId, Sms sms)
        {
            var response = await _http.PostAsJsonAsync($"api/sms/group/{groupId}", sms);

            var providerSent = (await response.Content.ReadFromJsonAsync<ServiceResponse<string>>()).Message;
            _logger.LogInformation(providerSent);
        }

        public async Task SendSMS(Sms sms)
        {
            var response = await _http.PostAsJsonAsync("api/sms", sms);

            var providerSent = (await response.Content.ReadFromJsonAsync<ServiceResponse<string>>()).Message;
            _logger.LogInformation(providerSent);
        }
    }
}
