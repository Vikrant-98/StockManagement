using Systematix.WebAPI.Models.DTO.Holdings;
using Systematix.WebAPI.Models.DTO.Ledger;
using Systematix.WebAPI.Repositories.LedgerRepository;

namespace Systematix.WebAPI.Business
{
    public class LedgerBusiness : ILedgerBusiness
    {
        private readonly ILedgerRepository _ledgerRepository;

        public LedgerBusiness(ILedgerRepository ledgerRepository) 
        {
            _ledgerRepository = ledgerRepository;
        }

        public async Task<(bool, string)> AddLedgerAsync(double ledger, string clientCode)
        {
            if (string.IsNullOrEmpty(clientCode))
            {
                return (false, "client Code not exist or valid try again ......");
            }
            
            if (ledger < 0)
            {
                return (false, "Enter valid Ledger...");
            }

            Ledger details = new Ledger() 
            {
                CreatedDate = DateTime.Now,
                ClientCode = clientCode,
                LedgerBalance = ledger,
                Status = 1,
                IsDelete = false,
            };

            var Results = await _ledgerRepository.AddLedgerAsync(details).ConfigureAwait(false);
            return Results;
        }
        public async Task<(bool, double, string)> GetLedgerAsync(string clientCode)
        {
            if (string.IsNullOrEmpty(clientCode))
            {
                return (false,0 ,"client Code not exist or valid try again ......");
            }
            

            var Results = await _ledgerRepository.GetLedgerAsync(clientCode).ConfigureAwait(false);
            return Results;
        }

        public async Task<(bool, List<BranchRequest>)> GetBranchAsync()
        {
            var Results = await _ledgerRepository.GetBranchAsync().ConfigureAwait(false);
            return Results;
        }
        public async Task<(bool, string)> AddBranchAsync(BranchRequest branch)
        {
            try
            {

                if (string.IsNullOrEmpty(branch.BranchCode) || string.IsNullOrEmpty(branch.BranchName))
                {
                    return (false, "BranchCode and BranchName is not valid");
                }

                Branch branchreq = new Branch() 
                {
                    BranchCode = branch.BranchCode,
                    BranchName = branch.BranchName,
                    CreatedDate = DateTime.Now,
                    Status = 1,
                    IsDelete = false
                };
                var result = await _ledgerRepository.AddBranchAsync(branchreq).ConfigureAwait(false);

                return result;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
