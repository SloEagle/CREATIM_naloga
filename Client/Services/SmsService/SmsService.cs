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

        public async Task SendGroupSMS(int groupId, Sms sms)
        {
            var response = await _http.PostAsJsonAsync($"api/sms/{groupId}", sms);

            //Add logging
            var providerSent = (await response.Content.ReadFromJsonAsync<ServiceResponse<string>>()).Message;
            Console.WriteLine(providerSent);
        }

        public async Task SendSMS(Sms sms)
        {
            var response = await _http.PostAsJsonAsync("api/sms", sms);

            //Add logging
            var providerSent = (await response.Content.ReadFromJsonAsync<ServiceResponse<string>>()).Message;
            Console.WriteLine(providerSent);
        }
    }
}
