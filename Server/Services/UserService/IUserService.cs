using CREATIM_naloga.Shared.Models;

namespace CREATIM_naloga.Server.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<List<User>>> GetUsers();
        Task<ServiceResponse<User>> GetUser(int id);
        Task<ServiceResponse<List<User>>> AddUser(User user);
        Task<ServiceResponse<List<User>>> UpdateUser(User user);
        Task<ServiceResponse<List<User>>> DeleteUser(int id);
    }
}
