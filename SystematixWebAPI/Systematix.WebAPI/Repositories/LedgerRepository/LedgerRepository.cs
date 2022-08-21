using Systematix.WebAPI.Data;
using Systematix.WebAPI.Models.DTO.Holdings;
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

        public async Task<(bool, string)> AddBranchAsync(Branch branch)
        {
            try
            {
                var result = systematixDbContext.tbl_BranchMaster.Where(x => x.BranchCode == branch.BranchCode).FirstOrDefault();
                if (result != null)
                {
                    return (false, "BranchCode Already Exist");
                }
                await systematixDbContext.tbl_BranchMaster.AddAsync(branch);
                await systematixDbContext.SaveChangesAsync();

                return (true, "Branch Added Succesfully");
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

        public async Task<(bool, List<BranchRequest>)> GetBranchAsync()
        {
            try
            {
                List<BranchRequest> branches = new List<BranchRequest>();
                var result = systematixDbContext.tbl_BranchMaster.ToList();
                foreach (var item in result)
                {
                    branches.Add(new BranchRequest() 
                    {
                        BranchCode = item.BranchCode,
                        BranchName = item.BranchName
                    });
                }
                return (true, branches);
            }
            catch (Exception ex)
            {

                throw;
            }

        }

    }
}
