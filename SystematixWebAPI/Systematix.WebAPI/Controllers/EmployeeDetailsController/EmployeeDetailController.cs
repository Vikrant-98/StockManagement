using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Systematix.WebAPI.Models.EmployeeDetailsInfo;
using Systematix.WebAPI.Repositories.EmployeeDetailsRepositories;

namespace Systematix.WebAPI.Controllers.EmployeeDetailsController
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class EmployeeDetailController : ControllerBase
    {
        #region Pass Static Data
        //private readonly IEmployeeDetailRepository employeeDetailRepository;
        //DateTime dt = new DateTime();
        //[HttpGet]
        //public IActionResult GetAllEmployees()
        //{
        //    var EmployeeDetails = new List<EmployeeDetail>()
        //    {
        //        new EmployeeDetail
        //        {
        //            ID=1,
        //            FullName="Sushant Sandeep Wadekar",
        //            Address="Padel Canteen, Devgad",
        //            City="Devgad",
        //            MobileNumber="7875002870",
        //            Salary=15000.00,
        //            DOB= dt = new DateTime(1992, 12, 20),
        //            DOJ= dt = new DateTime(2022, 05, 01),
        //            HighestEducation = "MCA"
        //        },
        //        new EmployeeDetail
        //        {
        //            ID=2,
        //            FullName="Sagar R. Vaydande",
        //            Address="Kurla",
        //            City="Navi Mumbai",
        //            MobileNumber="7875002850",
        //            Salary=20000.00,
        //            DOB= dt = new DateTime(1993, 11, 15),
        //            DOJ= dt = new DateTime(2022, 04, 15),
        //            HighestEducation = "BSC"
        //        }

        //     };
        //    return Ok(EmployeeDetails);
        //}
        #endregion

        private readonly IEmployeeDetailRepository employeeDetailRepository;
        private readonly IMapper mapper;
        public EmployeeDetailController(IEmployeeDetailRepository EmployeeDetailRepository, IMapper mapper)
        {
            employeeDetailRepository = EmployeeDetailRepository;
            this.mapper = mapper;

        }

        public IMapper Mapper { get; }

        //[HttpGet]
        //public IActionResult Test()
        //{
        //    return Ok("Test Data");
        //}

        [HttpGet]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var emps = await employeeDetailRepository.GetAllEmployeesAsync();

            #region Without Use Automapper
            //Return DTO EMployeeDetails
            //    var empsDTO = new List<Models.DTO.EmployeeDetail>();
            //    emp.ToList().ForEach(O =>
            //    {
            //        var empDTO = new Models.DTO.EmployeeDetail()
            //        {
            //            ID = O.ID,
            //            FullName = O.FullName,
            //            Address = O.Address,
            //            City = O.City,
            //            MobileNumber = O.MobileNumber,
            //            Salary = O.Salary,
            //            DOB = O.DOB,
            //            DOJ = O.DOJ,
            //            HighestEducation = O.HighestEducation
            //        };
            //        empsDTO.Add(empDTO);
            //});
            #endregion

            var empsDTO = mapper.Map<List<Models.DTO.EmployeeDetailsDTO.EmployeeDetail>>(emps);

            return Ok(empsDTO);
        }

        [HttpGet]
        [Route("{id}")]
        [ActionName("GetEmployeesAsync")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> GetEmployeesAsync(int id)
        {
            var employee = await employeeDetailRepository.GetAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            var employeeDTO = mapper.Map<Models.DTO.EmployeeDetailsDTO.EmployeeDetail>(employee);

            return Ok(employeeDTO);
        }

        [HttpPost]
        [Authorize]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddEmployeeAsync(Models.DTO.EmployeeDetailsDTO.AddEmployeeDetailRequest addEmployeeDetailRequest)
        {
            //Validate EmployeeDetailsRequest
            //if (!ValidateAddEmployeeAsync(addEmployeeDetailRequest))
            //{
            //    return BadRequest(ModelState);
            //}

            // Request(DTO) to EmployeeInfo Model
            var emplooyee = new ClientDetail()
            {
                FullName = addEmployeeDetailRequest.FullName,
                Address = addEmployeeDetailRequest.Address,
                City = addEmployeeDetailRequest.City,
                MobileNumber = addEmployeeDetailRequest.MobileNumber,
                Salary = addEmployeeDetailRequest.Salary,
                DOB = addEmployeeDetailRequest.DOB,
                DOJ = addEmployeeDetailRequest.DOJ,
                HighestEducation = addEmployeeDetailRequest.HighestEducation
            };

            // Pass Details to Repository

            emplooyee = await employeeDetailRepository.AddEmployeeAsync(emplooyee);

            // Convert back to DTO

            var emplooyeeDTO = new ClientDetail()
            {
                ID = emplooyee.ID,
                FullName = emplooyee.FullName,
                Address = emplooyee.Address,
                City = emplooyee.City,
                MobileNumber = emplooyee.MobileNumber,
                Salary = emplooyee.Salary,
                DOB = emplooyee.DOB,
                DOJ = emplooyee.DOJ,
                HighestEducation = emplooyee.HighestEducation
            };

            return CreatedAtAction(nameof(GetEmployeesAsync), new { id = emplooyeeDTO.ID }, emplooyeeDTO);
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEmployeeAsync(int id)
        {
            // Get Employee Details from Database
            var employee = await employeeDetailRepository.DeleteEmployeeAsync(id);

            // If Null Found
            if (employee == null)
            {
                return NotFound();
            }

            // Convert response back to DTO
            var employeeDTO = new Models.DTO.EmployeeDetailsDTO.EmployeeDetail
            {
                ID = employee.ID,
                FullName = employee.FullName,
                Address = employee.Address,
                City = employee.City,
                MobileNumber = employee.MobileNumber,
                Salary = employee.Salary,
                DOB = employee.DOB,
                DOJ = employee.DOJ,
                HighestEducation = employee.HighestEducation
            };

            // RETURN Ok REsponse
            return Ok(employeeDTO);
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateEmployeeAsync([FromRoute] int id, [FromBody] Models.DTO.EmployeeDetailsDTO.UpdateEmployeeDetailRequest _updateEmployeeDetailRequest)
        {
            // Convert DTO to EmployeeInfo model
            var _emplooyee = new ClientDetail()
            {
                FullName = _updateEmployeeDetailRequest.FullName,
                Address = _updateEmployeeDetailRequest.Address,
                City = _updateEmployeeDetailRequest.City,
                MobileNumber = _updateEmployeeDetailRequest.MobileNumber,
                Salary = _updateEmployeeDetailRequest.Salary,
                DOB = _updateEmployeeDetailRequest.DOB,
                DOJ = _updateEmployeeDetailRequest.DOJ,
                HighestEducation = _updateEmployeeDetailRequest.HighestEducation
            };

            // up[date employeeDetail using repository

            _emplooyee = await employeeDetailRepository.UpdateEmployeeAsync(id, _emplooyee);

            // if null then not found

            if (_emplooyee == null)
            {
                return NotFound();
            }

            // convert EMployeeInfo back to DTO

            var emplooyeeDTO = new Models.DTO.EmployeeDetailsDTO.EmployeeDetail
            {
                ID = _emplooyee.ID,
                FullName = _emplooyee.FullName,
                Address = _emplooyee.Address,
                City = _emplooyee.City,
                MobileNumber = _emplooyee.MobileNumber,
                Salary = _emplooyee.Salary,
                DOB = _emplooyee.DOB,
                DOJ = _emplooyee.DOJ,
                HighestEducation = _emplooyee.HighestEducation
            };

            // return ok response
            return Ok(emplooyeeDTO);
        }

        #region Private Methods
        private bool ValidateAddEmployeeAsync(Models.DTO.EmployeeDetailsDTO.AddEmployeeDetailRequest addEmployeeDetailRequest)
        {
            if (addEmployeeDetailRequest == null)
            {
                ModelState.AddModelError(nameof(addEmployeeDetailRequest),
                        $"Add employee detail data is required.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(addEmployeeDetailRequest.FullName))
            {
                ModelState.AddModelError(nameof(addEmployeeDetailRequest.FullName),
                    $"{nameof(addEmployeeDetailRequest.FullName)} cannot be null or empty or white space.");
            }

            if (string.IsNullOrWhiteSpace(addEmployeeDetailRequest.Address))
            {
                ModelState.AddModelError(nameof(addEmployeeDetailRequest.Address),
                    $"{nameof(addEmployeeDetailRequest.Address)} cannot be null or empty or white space.");
            }

            if (string.IsNullOrWhiteSpace(addEmployeeDetailRequest.City))
            {
                ModelState.AddModelError(nameof(addEmployeeDetailRequest.City),
                    $"{nameof(addEmployeeDetailRequest.City)} cannot be null or empty or white space.");
            }

            if (string.IsNullOrWhiteSpace(addEmployeeDetailRequest.MobileNumber))
            {
                ModelState.AddModelError(nameof(addEmployeeDetailRequest.MobileNumber),
                    $"{nameof(addEmployeeDetailRequest.MobileNumber)} cannot be null or empty or white space.");
            }

            if (addEmployeeDetailRequest.Salary >= 0)
            {
                ModelState.AddModelError(nameof(addEmployeeDetailRequest.Salary),
                    $"{nameof(addEmployeeDetailRequest.Salary)} cannot be less and equal to Zero.");
            }

            //if (DateTime.TryParseExact(addEmployeeDetailRequest.DOB.ToString(), "dd/MM/yyyy",))
            //{
            //    ModelState.AddModelError(nameof(addEmployeeDetailRequest.MobileNumber),
            //        $"{nameof(addEmployeeDetailRequest.MobileNumber)} cannot be null or empty or white space.");
            //}

            if (string.IsNullOrWhiteSpace(addEmployeeDetailRequest.HighestEducation))
            {
                ModelState.AddModelError(nameof(addEmployeeDetailRequest.HighestEducation),
                    $"{nameof(addEmployeeDetailRequest.HighestEducation)} cannot be null or empty or white space.");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }
        #endregion

    }
}
