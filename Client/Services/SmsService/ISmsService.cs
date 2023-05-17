namespace CREATIM_naloga.Client.Services.SmsService
{
    public interface ISmsService
    {
        Provider Provider { get; set; }
        Task GetProvider();
        Task SendSMS(Sms sms);
        Task SendGroupSMS(int groupId, Sms sms);
    }
}
