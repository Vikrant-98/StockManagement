using Systematix.WebAPI.Models.DTO.Ledger;

namespace Systematix.WebAPI.Repositories.LedgerRepository
{
    public interface ILedgerRepository
    {
        Task<(bool, string)> AddLedgerAsync(Ledger Ledger);
        Task<(bool, double, string)> GetLedgerAsync(string clientCode);
    }
}
