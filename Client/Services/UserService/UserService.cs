using System.Net.Http.Json;

namespace CREATIM_naloga.Client.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly HttpClient _http;

        public UserService(HttpClient http)
        {
            _http = http;
        }

        public List<User> Users { get; set; } = new List<User>();
        public User User { get; set; } = new User();

        public async Task AddUser(User user)
        {
            var response = await _http.PostAsJsonAsync("api/user", user);
            Users = (await response.Content.ReadFromJsonAsync<ServiceResponse<List<User>>>()).Data;
        }

        public async Task DeleteUser(int id)
        {
            var response = await _http.DeleteAsync($"api/user/{id}");
            Users = (await response.Content.ReadFromJsonAsync<ServiceResponse<List<User>>>()).Data;
        }

        public async Task GetUser(int id)
        {
            var response = await _http.GetFromJsonAsync<ServiceResponse<User>>($"api/user/{id}");
            if(response != null && response.Data != null)
            {
                User = response.Data;
            }
            else
            {
                User = new User();
            }
        }

        public async Task GetUsers()
        {
            var response = await _http.GetFromJsonAsync<ServiceResponse<List<User>>>("api/user");
            if(response != null && response.Data != null)
            {
                Users = response.Data;
            }
        }

        public async Task UpdateUser(User user)
        {
            var response = await _http.PutAsJsonAsync("api/user", user);
            Users = (await response.Content.ReadFromJsonAsync<ServiceResponse<List<User>>>()).Data;
        }
    }
}
