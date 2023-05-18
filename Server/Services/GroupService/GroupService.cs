using CREATIM_naloga.Client.Pages;
using CREATIM_naloga.Server.Data;
using CREATIM_naloga.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace CREATIM_naloga.Server.Services.GroupService
{
    public class GroupService : IGroupService
    {
        private readonly DataContext _context;
        private readonly ILogger<GroupService> _logger;

        public GroupService(DataContext context, ILogger<GroupService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<ServiceResponse<List<Group>>> AddGroup(Group group)
        {
            try
            {
                _context.Groups.Add(group);
                await _context.SaveChangesAsync();
                return await GetGroups();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return new ServiceResponse<List<Group>>
                {
                    Success = false,
                    Message = $"Server side error: {ex.ToString()}"
                };
            }
        }

        public async Task<ServiceResponse<List<Group>>> DeleteGroup(int id)
        {
            try
            {
                var group = await GetGroup(id);

                if (group.Data == null)
                {
                    return new ServiceResponse<List<Group>>
                    {
                        Success = false,
                        Message = "User not found"
                    };
                }
                else
                {
                    _context.Groups.Remove(group.Data);
                    await _context.SaveChangesAsync();
                    return await GetGroups();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return new ServiceResponse<List<Group>>
                {
                    Success = false,
                    Message = $"Server side error: {ex.ToString()}"
                };
            }
        }

        public async Task<ServiceResponse<Group>> GetGroup(int id)
        {
            try
            {
                var group = await _context.Groups
                    .FirstOrDefaultAsync(g => g.Id == id);

                if (group == null)
                {
                    return new ServiceResponse<Group>
                    {
                        Success = false,
                        Message = "User not found"
                    };
                }
                else
                {
                    return new ServiceResponse<Group>
                    {
                        Data = group,
                        Success = true
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return new ServiceResponse<Group>
                {
                    Success = false,
                    Message = $"Server side error: {ex.ToString()}"
                };
            }
        }

        public async Task<ServiceResponse<List<Group>>> GetGroups()
        {
            try
            {
                var groups = await _context.Groups
                    .OrderBy(g => g.Name)
                    .ToListAsync();

                return new ServiceResponse<List<Group>>
                {
                    Data = groups,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return new ServiceResponse<List<Group>>
                {
                    Success = false,
                    Message = $"Server side error: {ex.ToString()}"
                };
            }
        }

        public async Task<ServiceResponse<List<Group>>> UpdateGroup(Group group)
        {
            try
            {
                var dbGroup = await GetGroup(group.Id);
                if (dbGroup.Data == null)
                {
                    return new ServiceResponse<List<Group>>
                    {
                        Success = false,
                        Message = "User not found"
                    };
                }
                else
                {
                    dbGroup.Data.Name = group.Name;

                    await _context.SaveChangesAsync();

                    return await GetGroups();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return new ServiceResponse<List<Group>>
                {
                    Success = false,
                    Message = $"Server side error: {ex.ToString()}"
                };
            }
        }
    }
}
