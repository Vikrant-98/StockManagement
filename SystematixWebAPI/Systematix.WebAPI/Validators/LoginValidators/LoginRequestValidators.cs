using FluentValidation;
using Systematix.WebAPI.Models.DTO.LoginDTO;

namespace Systematix.WebAPI.Validators.LoginValidators
{
    public class LoginRequestValidators : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidators()
        {
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
