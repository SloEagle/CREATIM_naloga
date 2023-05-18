using CREATIM_naloga.Client.Pages;
using CREATIM_naloga.Shared.Models;
using System.Net.Http.Json;

namespace CREATIM_naloga.Client.Services.GroupService
{
    public class GroupService : IGroupService
    {
        private readonly HttpClient _http;

        public GroupService(HttpClient http)
        {
            _http = http;
        }

        public List<Group> Groups { get; set; }
        public Group Group { get; set; }

        public async Task AddGroup(Group group)
        {
            var response = await _http.PostAsJsonAsync("api/group", group);
            Groups = (await response.Content.ReadFromJsonAsync<ServiceResponse<List<Group>>>()).Data;
        }

        public async Task DeleteGroup(int id)
        {
            var response = await _http.DeleteAsync($"api/group/{id}");
            Groups = (await response.Content.ReadFromJsonAsync<ServiceResponse<List<Group>>>()).Data;
        }

        public async Task GetGroup(int id)
        {
            var response = await _http.GetFromJsonAsync<ServiceResponse<Group>>($"api/group/{id}");
            if (response != null && response.Data != null)
            {
                Group = response.Data;
            }
            else
            {
                Group = new Group();
            }
        }

        public async Task GetGroups()
        {
            var response = await _http.GetFromJsonAsync<ServiceResponse<List<Group>>>("api/group");
            if (response != null && response.Data != null)
            {
                Groups = response.Data;
            }
        }

        public async Task UpdateGroup(Group group)
        {
            var response = await _http.PutAsJsonAsync("api/group", group);
            Groups = (await response.Content.ReadFromJsonAsync<ServiceResponse<List<Group>>>()).Data;
        }
    }
}
