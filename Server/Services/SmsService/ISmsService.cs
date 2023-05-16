using CREATIM_naloga.Shared.Models;

namespace CREATIM_naloga.Server.Services.SmsService
{
    public interface ISmsService
    {
        Task SendSMS();
        Task SendGroupSMS(int groupId);
        Task<Provider> GetTopProvider();
    }
}
