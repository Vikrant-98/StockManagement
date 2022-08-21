using Systematix.WebAPI.Models.EmployeeDetailsInfo;

namespace Systematix.WebAPI.Repositories.EmployeeDetailsRepositories
{
    public interface IEmployeeDetailRepository
    {
        Task<IEnumerable<ClientDetail>> GetAllEmployeesAsync();

        //Get by ID
        Task<ClientDetail> GetAsync(int ID);

        //Create POST
        Task<ClientDetail> AddEmployeeAsync(ClientDetail employeedetail);

        //Delete
        Task<ClientDetail> DeleteEmployeeAsync(int id);

        //Update
        Task<ClientDetail> UpdateEmployeeAsync(int id, ClientDetail employeeDetail);
    }
}
