using Systematix.WebAPI.Data;
using Systematix.WebAPI.Models.DTO.Ledger;

namespace Systematix.WebAPI.Repositories.LedgerRepository
{
    public class LedgerRepository : ILedgerRepository
    {
        private readonly SystematixDbContext systematixDbContext;
        public LedgerRepository(SystematixDbContext SystematixDbContext) 
        {
            systematixDbContext = SystematixDbContext;
        }

        public async Task<(bool, string)> AddLedgerAsync(Ledger Ledger)
        {
            try
            {
                var result = systematixDbContext.tbl_ClientLedger.Where(x => x.ClientCode == Ledger.ClientCode).FirstOrDefault();
                if (result == null)
                {
                    await systematixDbContext.tbl_ClientLedger.AddAsync(Ledger);
                    await systematixDbContext.SaveChangesAsync();
                }
                else
                {
                    result.LedgerBalance = result.LedgerBalance + Ledger.LedgerBalance;
                    result.UpdateDate = DateTime.Now;
                    systematixDbContext.tbl_ClientLedger.Update(result);
                    await systematixDbContext.SaveChangesAsync();
                }

                return (true, "Ledger Added Succesfully");
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        public async Task<(bool, double, string)> GetLedgerAsync(string clientCode)
        {
            try
            {
                var result = systematixDbContext.tbl_ClientLedger.Where(x => x.ClientCode == clientCode).FirstOrDefault();

                return (true, result.LedgerBalance, "Ledger Added Succesfully");
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

    }
}
