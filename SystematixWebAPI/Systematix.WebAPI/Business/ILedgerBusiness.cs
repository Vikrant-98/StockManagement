using Systematix.WebAPI.Models.DTO.Holdings;

namespace Systematix.WebAPI.Business
{
    public interface ILedgerBusiness
    {
        Task<(bool, string)> AddLedgerAsync(double ledger, string clientCode);
        Task<(bool, double, string)> GetLedgerAsync(string clientCode);
        Task<(bool, string)> AddBranchAsync(BranchRequest branch);
        Task<(bool, List<BranchRequest>)> GetBranchAsync();
    }
}
