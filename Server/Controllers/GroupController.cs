using CREATIM_naloga.Server.Services.GroupService;
using CREATIM_naloga.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CREATIM_naloga.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Group>>>> GetGroups()
        {
            var result = await _groupService.GetGroups();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ServiceResponse<Group>>> GetGroup(int id)
        {
            var result = await _groupService.GetGroup(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<Group>>>> AddGroup(Group group)
        {
            var result = await _groupService.AddGroup(group);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<Group>>>> UpdateGroup(Group group)
        {
            var result = await _groupService.UpdateGroup(group);
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ServiceResponse<List<Group>>>> DeleteGroup(int id)
        {
            var result = await _groupService.DeleteGroup(id);
            return Ok(result);
        }
    }
}
