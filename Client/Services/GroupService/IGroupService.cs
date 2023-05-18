namespace CREATIM_naloga.Client.Services.GroupService
{
    public interface IGroupService
    {
        List<Group> Groups { get; set; }
        Group Group { get; set; }
        Task GetGroups();
        Task GetGroup(int id);
        Task AddGroup(Group group);
        Task UpdateGroup(Group group);
        Task DeleteGroup(int id);
    }
}
