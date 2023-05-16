namespace CREATIM_naloga.Client.Services.SmsService
{
    public interface ISmsService
    {
        Provider Provider { get; set; }
        Task GetProvider();
    }
}
