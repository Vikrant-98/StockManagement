namespace Systematix.WebAPI.Business
{
    public interface ILedgerBusiness
    {
        Task<(bool, string)> AddLedgerAsync(double ledger, string clientCode);
        Task<(bool, double, string)> GetLedgerAsync(string clientCode);
    }
}
