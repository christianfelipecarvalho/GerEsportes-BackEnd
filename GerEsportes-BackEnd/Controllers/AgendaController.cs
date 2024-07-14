using GerEsportes_BackEnd.Aplicacao.AplicAgendas;
using GerEsportes_BackEnd.Dominios.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace GerEsportes_BackEnd.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class AgendaController : ControllerBase
    {
        private readonly IAplicAgenda _aplicAgenda;

        public AgendaController(IAplicAgenda aplicAgenda)
        {
            _aplicAgenda = aplicAgenda;
        }

        [Route("SalvarAgenda")]
        [HttpPost]
        public async Task<ActionResult<object>> SalvarAgenda([FromBody] List<SalvarAgendaDto> dto)
        {
            try
            {
                return Ok(_aplicAgenda.SalvarAgenda(dto));
            }
            catch (Exception e)
            {
                return StatusCode(500, $"{e.Message}");
            }
        }

        [Route("AlterarAgenda")]
        [HttpPut]
        public async Task<ActionResult<object>> AlterarAgenda([FromBody] List<AlterarAgendaDto> dtos)
        {
            try
            {
                return Ok(_aplicAgenda.AlterarAgenda(dtos));
            }
            catch (Exception e)
            {
                return StatusCode(500, $"{e.Message}");
            }
        }

        [Route("ListarTodasAgendas")]
        [HttpGet]
        public async Task<ActionResult<object>> ListarTodasAgendas()
        {
            try
            {
                return Ok(_aplicAgenda.ListarTodasAgendas());
            }
            catch (Exception e)
            {
                return StatusCode(500, $"{e.Message}");
            }
        }

        [Route("ListarAgendasPorId/{codigoUsuario}/{codigoAgenda}")]
        [HttpGet]
        public async Task<ActionResult<object>> ListarAgendasPorId(int codigoUsuario, int codigoAgenda)
        {
            try
            {
                return Ok(_aplicAgenda.ListarAgendasPorId(codigoUsuario, codigoAgenda));
            }
            catch (Exception e)
            {
                return StatusCode(500, $"{e.Message}");
            }
        }

        [Route("ExcluirAgenda/{codigoUsuario}/{codigoAgenda}")]
        [HttpDelete]
        public async Task<ActionResult<object>> ExcluirAgenda(int codigoUsuario, int codigoAgenda)
        {
            try
            {
                _aplicAgenda.ExcluirAgenda(codigoUsuario, codigoAgenda);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, $"{e.Message}");
            }
        }
    }
}
