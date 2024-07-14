using GerEsportes_BackEnd.Aplicacao.AplicDashboards;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GerEsportes_BackEnd.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class DashboardController : ControllerBase
    {

        private readonly IAplicDashboard _aplicDashboard;

        public DashboardController(IAplicDashboard aplicDashboard)
        {
            _aplicDashboard = aplicDashboard;
        }

        [Route("ListarAtletasPorModalidde")]
        [HttpGet]
        public async Task<ActionResult<object>> ListarAtletasPorModalidde()
        {
            try
            {
                return Ok(_aplicDashboard.AtletasPorModalidde());
            }
            catch (Exception e)
            {
                return StatusCode(500, $"{e.Message}");
            }
        }

        [Route("ListarAtletasMediaIdade")]
        [HttpGet]
        public async Task<ActionResult<object>> ListarAtletasMediaIdade()
        {
            try
            {
                return Ok(_aplicDashboard.AtletasMediaIdade());
            }
            catch (Exception e)
            {
                return StatusCode(500, $"{e.Message}");
            }
        }

        [Route("ListarAtletasGeneroFeminino")]
        [HttpGet]
        public async Task<ActionResult<object>> ListarAtletasGeneroFeminino()
        {
            try
            {
                return Ok(_aplicDashboard.AtletasGeneroFeminino());
            }
            catch (Exception e)
            {
                return StatusCode(500, $"{e.Message}");
            }
        }

        [Route("ListarMediasEPorcentagens")]
        [HttpGet]
        public async Task<ActionResult<object>> ListarMediasEPorcentagens()
        {
            try
            {
                return Ok(_aplicDashboard.MediasEPorcentagens());
            }
            catch (Exception e)
            {
                return StatusCode(500, $"{e.Message}");
            }
        }
    }
}
