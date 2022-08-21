using Microsoft.EntityFrameworkCore;
using Systematix.WebAPI.Data;
using Systematix.WebAPI.Models.EmployeeDetailsInfo;

namespace Systematix.WebAPI.Repositories.EmployeeDetailsRepositories
{
    public class EmployeeDetailRepository : IEmployeeDetailRepository
    {
        private readonly SystematixDbContext systematixDbContext;

        public EmployeeDetailRepository(SystematixDbContext SystematixDbContext)
        {
            systematixDbContext = SystematixDbContext;
        }

        //Add Employee Details
        public async Task<ClientDetail> AddEmployeeAsync(ClientDetail employeedetail)
        {
            await systematixDbContext.AddAsync(employeedetail);
            await systematixDbContext.SaveChangesAsync();
            return employeedetail;
        }

        //Delete
        public async Task<ClientDetail> DeleteEmployeeAsync(int id)
        {
            var employee = await systematixDbContext.tbl_EmployeeDetails.FirstOrDefaultAsync(x => x.ID == id);

            if (employee == null)
            {
                return null;
            }

            //Delete the Employee
            systematixDbContext.tbl_EmployeeDetails.Remove(employee);
            await systematixDbContext.SaveChangesAsync();
            return employee;
        }

        public async Task<IEnumerable<ClientDetail>> GetAllEmployeesAsync()
        {
            return await systematixDbContext.tbl_EmployeeDetails.ToListAsync();
        }

        public async Task<ClientDetail> GetAsync(int id)
        {
            return await systematixDbContext.tbl_EmployeeDetails.FirstOrDefaultAsync(x => x.ID == id);
        }

        //Update
        public async Task<ClientDetail> UpdateEmployeeAsync(int id, ClientDetail employeeDetail)
        {
            var employee = await systematixDbContext.tbl_EmployeeDetails.FirstOrDefaultAsync(x => x.ID == id);

            if (employee == null)
            {
                return null;
            }

            employee.FullName = employeeDetail.FullName;
            employee.Address = employeeDetail.Address;
            employee.City = employeeDetail.City;
            employee.MobileNumber = employeeDetail.MobileNumber;
            employee.DOB = employeeDetail.DOB;
            employee.DOJ = employeeDetail.DOJ;
            employee.Salary = employeeDetail.Salary;
            employee.HighestEducation = employeeDetail.HighestEducation;

            await systematixDbContext.SaveChangesAsync();

            return employee;

        }
    }
}
