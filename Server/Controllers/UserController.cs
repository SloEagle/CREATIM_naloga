using CREATIM_naloga.Server.Services.UserService;
using CREATIM_naloga.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CREATIM_naloga.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<User>>>> GetUsers()
        {
            var result = await _userService.GetUsers();
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ServiceResponse<User>>> GetUser(int id)
        {
            var result = await _userService.GetUser(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<User>>>> AddUser(User user)
        {
            var result = await _userService.AddUser(user);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<User>>>> UpdateUser(User user)
        {
            var result = await _userService.UpdateUser(user);
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ServiceResponse<List<User>>>> DeleteUser(int id)
        {
            var result = _userService.DeleteUser(id);
            return Ok(result);
        }
    }
}
