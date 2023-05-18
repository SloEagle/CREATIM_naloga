using CREATIM_naloga.Server.Data;
using CREATIM_naloga.Shared.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Xml.Linq;

namespace CREATIM_naloga.Server.Services.SmsService
{
    public class SmsService : ISmsService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _config;

        public Sms Sms { get; set; }

        public SmsService(DataContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<Provider> GetProvider()
        {
            return await _context.Providers.FirstOrDefaultAsync();
        }

        public async Task<ServiceResponse<string>> SendGroupSMS(int groupId, Sms sms)
        {
            var users = await _context.Users
                .Where(u => u.GroupId == groupId)
                .ToListAsync();

            foreach (var user in users)
            {
                await SendSMS(sms);
            }

            return new ServiceResponse<string> 
            {
                Message = $"Sent Group SMS\r\nTo group: {groupId}\r\nFrom: {sms.From}\r\nBody: {sms.Body}",
                Success = true
            };
        }

        public async Task<ServiceResponse<string>> SendSMS(Sms sms)
        {
            var providers = await _context.Providers
                    .FromSqlRaw(@"
                    DECLARE @TopProviderId INT;

                    SELECT TOP 1 @TopProviderId = Id
                    FROM Providers

                    IF EXISTS (
                        SELECT *
                        FROM Providers
                        WHERE SentCount >= 99
                    ) BEGIN
                        INSERT INTO Providers (Name, SentCount)
                        SELECT Name, 0
                        FROM Providers
                        WHERE Id = @TopProviderId;

	                    DELETE FROM Providers
                        WHERE Id = @TopProviderId;
                    END ELSE BEGIN
                        UPDATE Providers
                        SET SentCount = SentCount + 1
                        WHERE Id = @TopProviderId;
                    END;

                    SELECT TOP 1 *
                    FROM Providers")
                    .ToListAsync();

            if (providers.Count != 0)
            {
                var provider = providers[0];

                SendSmsUsingProvider(provider, sms);

                return new ServiceResponse<string>
                {
                    Message = $"Sent through provider: {provider.Name}\r\nTo: {sms.To}\r\nFrom: {sms.From}\r\nBody: {sms.Body}",
                    Success = true
                };
            }
            else
            {
                return new ServiceResponse<string>
                {
                    Message = "SMS not sent",
                    Success = false
                };
            }
        }

        private void SendSmsUsingProvider(Provider provider, Sms sms)
        {
            //Example purposes
            if(provider.Name == "Siol")
            {
                RestCommand(provider, sms);
            }
            else if(provider.Name == "Telekom")
            {
                SoapCommand(provider, sms);
            }
        }

        private void RestCommand(Provider provider, Sms sms)
        {
            var accountSid = provider.SID;
            var authToken = _config.GetSection($"Providers:Tokens:{provider.Name}").Value;
            var url = provider.Url;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{accountSid}:{authToken}")));

                var values = new Dictionary<string, string>
                {
                    { "To", sms.To },
                    { "From", sms.From },
                    { "Body", sms.Body }
                };

                var content = new FormUrlEncodedContent(values);

                //Disabled (testing)
                //var response = client.PostAsync(url, content).Result;
            }
        }

        private void SoapCommand(Provider provider, Sms sms)
        {
            var accountSid = provider.SID;
            var authToken = _config.GetSection($"Providers:Tokens:{provider.Name}").Value;
            var url = provider.Url;

            var xml = new XElement(provider.Name,
                new XElement("sendSms",
                    new XElement("accountSid", accountSid),
                    new XElement("authToken", authToken),
                    new XElement("message",
                        new XElement("to", sms.To),
                        new XElement("from", sms.From),
                        new XElement("body", sms.Body)
                    )
                )
            );

            using (var client = new HttpClient())
            {
                var content = new StringContent(xml.ToString(), Encoding.UTF8, "application/xml");

                //Disabled (testing)
                //var response = client.PostAsync(url, content).Result;
            }
        }
    }
}
