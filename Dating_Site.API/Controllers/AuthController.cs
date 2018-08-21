using System.Threading.Tasks;
using Dating_Site.API.Data;
using Dating_Site.API.Dtos;
using Dating_Site.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dating_Site.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        public AuthController(IAuthRepository repo)
        {
            _repo = repo;

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto) 
        {
            // validate request

            userForRegisterDto.Username = userForRegisterDto.Username.ToLower();

            if (await _repo.UserExists(userForRegisterDto.Username))
                return BadRequest("Username already exists");

                var userToCreate = new User
                {
                    UserName = userForRegisterDto.Username
                };

                var createdUser = await _repo.Register(userToCreate, userForRegisterDto.Password);
                

                return StatusCode(201);
        }
    }
}