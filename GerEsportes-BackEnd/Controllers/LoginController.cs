using GerEsportes_BackEnd.Aplicacao.AplicLogins;
using GerEsportes_BackEnd.Dominios.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GerEsportes_BackEnd.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IAplicLogin _aplicLogin;

        public LoginController(IAplicLogin aplicLogin)
        {
            _aplicLogin = aplicLogin;
        }

        [Route("Login")]
        [HttpPut]
        public async Task<ActionResult<IEnumerable<object>>> EnviarEmail([FromBody] LoginDto dto)
        {
            try
            {
                return Ok(_aplicLogin.Login(dto));
            }
            catch (Exception e)
            {
                return StatusCode(500, $"{e.Message}");
            }
        }

        [Route("Refresh")]
        [HttpPut]
        public async Task<ActionResult<IEnumerable<object>>> Refresh([FromBody] RefreshDto dto)
        {
            try
            {
                return Ok(_aplicLogin.Refresh(dto));
            }
            catch (Exception e)
            {
                return StatusCode(500, $"{e.Message}");
            }
        }
    }
}
