using CREATIM_naloga.Server.Services.SmsService;
using CREATIM_naloga.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CREATIM_naloga.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmsController : ControllerBase
    {
        private readonly ISmsService _smsService;

        public SmsController(ISmsService smsService)
        {
            _smsService = smsService;
        }

        [HttpGet]
        public async Task<ActionResult<Provider>> GetProvider()
        {
            var result = await _smsService.GetProvider();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<string>>> SendSMS(Sms sms)
        {
            var result = await _smsService.SendSMS(sms);
            return Ok(result);
        }

        [HttpPost("group/{groupId}")]
        public async Task<ActionResult<ServiceResponse<string>>> SendGroupSMS(int groupId, Sms sms)
        {
            var result = await _smsService.SendGroupSMS(groupId, sms);
            return Ok(result);
        }
    }
}
