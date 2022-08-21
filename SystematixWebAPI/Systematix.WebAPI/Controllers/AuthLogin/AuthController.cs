using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Systematix.WebAPI.Models.DTO.LoginDTO;
using Systematix.WebAPI.Repositories.TokenHandlerRepositories;
using Systematix.WebAPI.Repositories.UserRepositories;

namespace Systematix.WebAPI.Controllers.AuthLogin
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly ITokenHandler tokenHandler;

        public AuthController(IUserRepository userRepository, ITokenHandler tokenHandler)
        {
            this.userRepository = userRepository;
            this.tokenHandler = tokenHandler;
        }

        public ITokenHandler TokenHandler { get; }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginAsync(LoginRequest loginRequest)
        {
            //Validate Login Request - run Default

            // check if user is authenticated
            // check username and password
            var user = await userRepository.AuthenticateAsync(loginRequest.UserName, loginRequest.Password);

            if (user != null)
            {
                //Generate Jwt Token
                var token = await tokenHandler.CreateTokenAsync(user);
                //return Ok(token);
                return Ok(new { Token = token });

            }

            return BadRequest("Username or Password is incorrect.");
        }
    }
}
