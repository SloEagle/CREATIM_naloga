using CREATIM_naloga.Shared.Models;

namespace CREATIM_naloga.Server.Services.SmsService
{
    public interface ISmsService
    {
        Sms Sms { get; set; }
        Task<Provider> GetProvider();
        Task<ServiceResponse<string>> SendSMS(Sms sms);
        Task<ServiceResponse<string>> SendGroupSMS(int groupId, Sms sms);
    }
}
