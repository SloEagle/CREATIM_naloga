namespace CREATIM_naloga.Client.Services.UserService
{
    public interface IUserService
    {
        List<User> Users { get; set; }
        User User { get; set; }
        Task GetUsers();
        Task GetUser(int id);
        Task AddUser(User user);
        Task UpdateUser(User user);
        Task DeleteUser(int id);
    }
}
