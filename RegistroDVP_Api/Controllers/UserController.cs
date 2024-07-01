using Microsoft.AspNetCore.Mvc;
using RegistroDVP_Api.Models.Dominio;
using RegistroDVP_Api.Repostitory.Interfaz;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RegistroDVP_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository repo;
        public UserController(IUserRepository repo)
        {
            this.repo = repo;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto logindto)
        {
            ResponseDto response = new ResponseDto();
            response = await repo.Login(logindto);
            if (response.success)
            {
                return Ok(response);
            }
            else
            {
                return NotFound(response);
            }
        }

        [HttpPost]
        [Route("Insert")]
        public async Task<IActionResult> InsertUser([FromBody] PersonDto personDto)
        {
            ResponseDto response = new ResponseDto("", true);
            response = await repo.InsertPersonAndUser(personDto);
            if (response.success)
            {
                return Ok(response);
            }
            else
            {
                return NotFound(response);
            }
        }

    }
}
