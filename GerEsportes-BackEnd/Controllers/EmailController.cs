using GerEsportes_BackEnd.Aplicacao.AplicEmails;
using GerEsportes_BackEnd.Dominios.Dtos;
using GerEsportes_BackEnd.Dominios.Emails;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GerEsportes_BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController : ControllerBase
    {

        private readonly IAplicEmail _aplicEmail;

        public EmailController(IAplicEmail aplicEmail)
        {
            _aplicEmail = aplicEmail;
        }

        [Route("EsqueciMinhaSenha")]
        [HttpPut]
        public async Task<ActionResult<IEnumerable<object>>> EnviarEmail([FromBody] EmailDto dto)
        {
            try
            {
                _aplicEmail.EsqueciMinhaSenha(dto);

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, $"{e.Message}");
            }
        }

        [Route("ValidarCodigoRecuperacao")]
        [HttpPost]
        public async Task<ActionResult<int>> ValidarCodigoRecuperacao([FromBody] ValidarRecuperacaoDto dto)
        {
            try
            {
                return Ok(_aplicEmail.ValidarCodigoRecuperacao(dto));
            }
            catch (Exception e)
            {
                return StatusCode(500, $"{e.Message}");
            }
        }
    }
}
