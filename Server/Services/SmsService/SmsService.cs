using CREATIM_naloga.Server.Data;
using CREATIM_naloga.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace CREATIM_naloga.Server.Services.SmsService
{
    public class SmsService : ISmsService
    {
        private readonly DataContext _context;

        public SmsService(DataContext context)
        {
            _context = context;
        }

        public async Task<Provider> GetTopProvider()
        {
            //dela z enostavnim querijem!
            return await _context.Providers
                .FromSql($"DECLARE @TopProviderId INT; SELECT TOP 1 @TopProviderId = Id FROM Providers IF EXISTS (SELECT * FROM Providers WHERE SentCount >= 100 ) BEGIN INSERT INTO Providers (Name, SentCount) SELECT Name, 0 FROM Providers WHERE Id = @TopProviderId; DELETE FROM Providers WHERE Id = @TopProviderId; END ELSE BEGIN UPDATE Providers SET SentCount = SentCount + 1 WHERE Id = @TopProviderId; END; SELECT TOP 1 * FROM Providers")
                .FirstOrDefaultAsync();

        }

        public Task SendGroupSMS(int groupId)
        {
            throw new NotImplementedException();
        }

        public Task SendSMS()
        {
            throw new NotImplementedException();
        }
    }
}
