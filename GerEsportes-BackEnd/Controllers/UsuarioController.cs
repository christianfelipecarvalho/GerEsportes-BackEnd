using GerEsportes_BackEnd.Aplicacao.AplicUsuarios;
using GerEsportes_BackEnd.Dominios.Dtos;
using GerEsportes_BackEnd.Dominios.Usuarios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GerEsportes_BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UsuarioController : ControllerBase
    {
        private readonly IAplicUsuario _aplicUsuario;

        public UsuarioController(IAplicUsuario aplicUsuario)
        {
            _aplicUsuario = aplicUsuario;
        }

        [Route("SalvarUsuario/{codigoUsuarioLogado}")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Usuario>>> SalvarUsuario(UsuarioDto dto, int codigoUsuarioLogado)
        {
            try
            {
                _aplicUsuario.SalvarUsuario(dto, codigoUsuarioLogado);
                return Ok("Usuário salvo com sucesso!");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"{e.Message}");
            }
        }

        [Route("AlterarUsuario/{codigoUsuarioLogado}")]
        [HttpPut]
        public async Task<ActionResult<object>> AlterarUsuario(AlterarUsuarioDto dto,int codigoUsuarioLogado)
        {
            try
            {
                _aplicUsuario.AlterarUsuario(dto, codigoUsuarioLogado);
                return Ok("Usuário alterado com sucesso!");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"{e.Message}");
            }
        }

        [Route("ListarUsuario/{id}")]
        [HttpGet]
        public async Task<ActionResult<object>> ListarUsuario(int id)
        {
            try
            {
                return Ok(_aplicUsuario.ListarUsuario(id));
            }
            catch (Exception e)
            {
                return StatusCode(500, $"{e.Message}");
            }
        }

        [Route("ListarUsuarioPorModalidade")]
        [HttpGet]
        public async Task<ActionResult<object>> ListarUsuarioPorModalidade(int id)
        {
            try
            {
                return Ok(_aplicUsuario.ListarUsuarioPorModalidade(id));
            }
            catch (Exception e)
            {
                return StatusCode(500, $"{e.Message}");
            }
        }


        [Route("ListarTodosUsuarios/{codigoUsuarioLogado}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> ListarTodosUsuarios(int codigoUsuarioLogado)
        {
            try
            {
                return Ok(_aplicUsuario.ListarTodosUsuarios(codigoUsuarioLogado));
            }
            catch (Exception e)
            {
                return StatusCode(500, $"{e.Message}");
            }
        }

        [Route("InativarUsuario/{id}/{codigoUsuarioLogado}")]
        [HttpPut]
        public async Task<ActionResult<IEnumerable<object>>> InativarUsuario(int id, int codigoUsuarioLogado)
        {
            try
            {
                _aplicUsuario.InativarUsuario(id, codigoUsuarioLogado);
                return Ok("Operação realizada!");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"{e.Message}");
            }
        }

        [Route("SalvarSenha")]
        [HttpPut]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<object>>> SalvarSenha(SalvarSenhaDto dto)
        {
            try
            {
                _aplicUsuario.SalvarSenha(dto);
                return Ok("Senha salva com sucesso!");
            }
            catch (Exception e)
            {
                return StatusCode(500, $"{e.Message}");
            }
        }

        [Route("UploadDocumento/{codigoUsuarioLogado}")]
        [HttpPost]
        public async Task<ActionResult<object>> UploadDocumento([FromBody] UploadDocumentoDto dto, int codigoUsuarioLogado)
        {
            try
            {
                return Ok(_aplicUsuario.UploadDocumento(dto, codigoUsuarioLogado));
            }
            catch (Exception e)
            {
                return StatusCode(500, $"{e.Message}");
            }
        }

        [Route("RecuperarImagemPerfil/{id}")]
        [HttpPost]
        public async Task<ActionResult<object>> RecuperarImagemPerfil(int id)
        {
            try
            {
                byte[] imagemBytes = _aplicUsuario.RecuperarImagemPerfil(id);

                if (imagemBytes == null)
                    return Ok();

                return File(imagemBytes, "image/png");

            }
            catch (Exception e)
            {
                return StatusCode(500, $"{e.Message}");
            }
        }

        [Route("DownloadArquivo/{codigoDocumento}")]
        [HttpPost]
        public async Task<ActionResult<object>> DownloadArquivo(int codigoDocumento)
        {
            try
            {
                return Ok(_aplicUsuario.DownloadArquivo(codigoDocumento));
            }
            catch (Exception e)
            {
                return StatusCode(500, $"{e.Message}");
            }
        }

        [Route("DeletarArquivo/{id}/{codigoUsuarioLogado}")]
        [HttpDelete]
        public async Task<ActionResult<object>> DeletarArquivo(int id, int codigoUsuarioLogado)
        {
            try
            {
                _aplicUsuario.DeletarArquivo(id, codigoUsuarioLogado);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, $"{e.Message}");
            }
        }
    }
}
