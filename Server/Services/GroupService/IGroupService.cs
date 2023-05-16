using CREATIM_naloga.Shared.Models;

namespace CREATIM_naloga.Server.Services.GroupService
{
    public interface IGroupService
    {
        Task<ServiceResponse<List<Group>>> GetGroups();
        Task<ServiceResponse<Group>> GetGroup(int id);
        Task<ServiceResponse<List<Group>>> AddGroup(Group group);
        Task<ServiceResponse<List<Group>>> UpdateGroup(Group group);
        Task<ServiceResponse<List<Group>>> DeleteGroup(int id);
    }
}
