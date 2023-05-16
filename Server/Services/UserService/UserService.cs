using CREATIM_naloga.Server.Data;
using CREATIM_naloga.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace CREATIM_naloga.Server.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<User>>> AddUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return await GetUsers();
        }

        public async Task<ServiceResponse<List<User>>> DeleteUser(int id)
        {
            var user = await GetUser(id);

            if(user.Data == null)
            {
                return new ServiceResponse<List<User>>
                {
                    Success = false,
                    Message = "User not found"
                };
            }
            else
            {
                _context.Users.Remove(user.Data);
                await _context.SaveChangesAsync();
                return await GetUsers();
            }
        }

        public async Task<ServiceResponse<User>> GetUser(int id)
        {
            var user = await _context.Users
                .Include(u => u.Group)
                .FirstOrDefaultAsync(u => u.Id == id);

            if(user == null)
            {
                return new ServiceResponse<User>
                {
                    Success = false,
                    Message = "User not found"
                };
            }
            else
            {
                return new ServiceResponse<User>
                {
                    Data = user,
                    Success = true
                };
            }
        }

        public async Task<ServiceResponse<List<User>>> GetUsers()
        {
            var users = await _context.Users
                .Include(u => u.Group)
                .OrderBy(u => u.Group)
                .ToListAsync();

            return new ServiceResponse<List<User>>
            {
                Data = users,
                Success = true
            };
        }

        public async Task<ServiceResponse<List<User>>> UpdateUser(User user)
        {
            var dbUser = await GetUser(user.Id);
            if(dbUser.Data == null)
            {
                return new ServiceResponse<List<User>>
                {
                    Success = false,
                    Message = "User not found"
                };
            }
            else
            {
                dbUser.Data.Name = user.Name;
                dbUser.Data.Email = user.Email;
                dbUser.Data.PhoneNumber = user.PhoneNumber;
                dbUser.Data.TaxNumber = user.TaxNumber;
                dbUser.Data.Bussiness = user.Bussiness;
                dbUser.Data.Group = user.Group;

                await _context.SaveChangesAsync();

                return await GetUsers();
            }
        }
    }
}
