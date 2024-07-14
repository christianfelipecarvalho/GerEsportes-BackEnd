using GerEsportes_BackEnd.Aplicacao.AplicUsuarios.Views;
using GerEsportes_BackEnd.Dominios.Dtos;

namespace GerEsportes_BackEnd.Aplicacao.AplicUsuarios
{
    public interface IAplicUsuario
    {
        void SalvarUsuario(UsuarioDto dto, int codigoUsuarioLogado);
        UsuarioView AlterarUsuario(AlterarUsuarioDto dto, int codigoUsuarioLogado);
        UsuarioView ListarUsuario(int id);
        List<UsuarioView> ListarTodosUsuarios(int codigoUsuarioLogado);
        void InativarUsuario(int idq, int codigoUsuarioLogado);
        void SalvarSenha(SalvarSenhaDto dto);
        string UploadDocumento(UploadDocumentoDto dto, int codigoUsuarioLogado);
        void DeletarArquivo(int id, int codigoUsuarioLogado);
        byte[] RecuperarImagemPerfil(int id);
        string DownloadArquivo(int id);
        List<UsuarioView> ListarUsuarioPorModalidade(int id);
    }
}
