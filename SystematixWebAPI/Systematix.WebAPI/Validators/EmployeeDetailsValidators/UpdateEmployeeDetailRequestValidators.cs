using FluentValidation;
using Systematix.WebAPI.Models.DTO.EmployeeDetailsDTO;

namespace Systematix.WebAPI.Validators.EmployeeDetailsValidators
{
    public class UpdateEmployeeDetailRequestValidators : AbstractValidator<UpdateEmployeeDetailRequest>
    {
        public UpdateEmployeeDetailRequestValidators()
        {
            RuleFor(x => x.FullName).NotEmpty();
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x.City).NotEmpty();
            RuleFor(x => x.MobileNumber).NotEmpty();
            RuleFor(x => x.Salary).GreaterThan(0);
            RuleFor(x => x.DOB).NotEmpty();
            RuleFor(x => x.DOJ).NotEmpty();
            RuleFor(x => x.HighestEducation).NotEmpty();
        }
    }
}
