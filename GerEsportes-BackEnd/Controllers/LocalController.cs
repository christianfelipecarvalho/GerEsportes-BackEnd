using GerEsportes_BackEnd.Aplicacao.AplicLocais;
using GerEsportes_BackEnd.Dominios.Dtos;
using GerEsportes_BackEnd.Dominios.Locais.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace GerEsportes_BackEnd.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class LocalController : ControllerBase
    {

        private readonly IAplicLocal _aplicLocal;

        public LocalController(IAplicLocal aplicLocal)
        {
            _aplicLocal = aplicLocal;
        }

        [Route("{id}/RecuperarLocalPorId")]
        [HttpGet]
        public async Task<ActionResult<object>> RecuperarLocalPorId(int id)
        {
            try
            {
                return Ok(_aplicLocal.ListarLocal(id));
            }
            catch (Exception e)
            {
                return StatusCode(500, $"{e.Message}");
            }
        }

        [Route("RecuperarTodosLocais")]
        [HttpGet]
        public async Task<ActionResult<object>> RecuperarTodosLocais()
        {
            try
            {
                return Ok(_aplicLocal.ListarTodosLocais());
            }
            catch (Exception e)
            {
                return StatusCode(500, $"{e.Message}");
            }
        }

        [Route("SalvarLocal")]
        [HttpPost]
        public async Task<ActionResult<object>> SalvarLocal(LocalDto dto)
        {
            try
            {
                _aplicLocal.SalvarLocal(dto);
                return Ok("Local salvo com sucesso!");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"{e.Message}");
            }
        }

        [Route("{id}/InativarLocal")]
        [HttpPut]
        public async Task<ActionResult<object>> InativarLocal(int id)
        {
            try
            {
                _aplicLocal.InativarLocal(id);
                return Ok("Local alterado com sucesso!");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"{e.Message}");
            }
        }

        [Route("AlterarLocal")]
        [HttpPut]
        public async Task<ActionResult<object>> AlterarLocal(AlterarLocalDto dto)
        {
            try
            {
                return Ok(_aplicLocal.AlterarLocal(dto));
            }
            catch (Exception e)
            {
                return StatusCode(500, $"{e.Message}");
            }
        }
    }
}
