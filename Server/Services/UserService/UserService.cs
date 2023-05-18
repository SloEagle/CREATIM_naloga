using CREATIM_naloga.Server.Data;
using CREATIM_naloga.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace CREATIM_naloga.Server.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly ILogger<UserService> _logger;

        public UserService(DataContext context, ILogger<UserService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ServiceResponse<List<User>>> AddUser(User user)
        {
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return await GetUsers();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return new ServiceResponse<List<User>>
                {
                    Success = false,
                    Message = $"Server side error: {ex.ToString()}"
                };
            }
        }

        public async Task<ServiceResponse<List<User>>> DeleteUser(int id)
        {
            try
            {
                var user = await GetUser(id);

                if (user.Data == null)
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
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return new ServiceResponse<List<User>>
                {
                    Success = false,
                    Message = $"Server side error: {ex.ToString()}"
                };
            }
        }

        public async Task<ServiceResponse<User>> GetUser(int id)
        {
            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Id == id);

                if (user == null)
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
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return new ServiceResponse<User>
                {
                    Success = false,
                    Message = $"Server side error: {ex.ToString()}"
                };
            }
        }

        public async Task<ServiceResponse<List<User>>> GetUsers()
        {
            try
            {
                var users = await _context.Users
                    .OrderBy(u => u.GroupId)
                    .Include(u => u.Group)
                    .ToListAsync();

                return new ServiceResponse<List<User>>
                {
                    Data = users,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return new ServiceResponse<List<User>>
                {
                    Success = false,
                    Message = $"Server side error: {ex.ToString()}"
                };
            }
        }

        public async Task<ServiceResponse<List<User>>> UpdateUser(User user)
        {
            try
            {
                var dbUser = await GetUser(user.Id);
                if (dbUser.Data == null)
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
                    dbUser.Data.GroupId = user.GroupId;

                    await _context.SaveChangesAsync();

                    return await GetUsers();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return new ServiceResponse<List<User>>
                {
                    Success = false,
                    Message = $"Server side error: {ex.ToString()}"
                };
            }
        }
    }
}
