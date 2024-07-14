using GerEsportes_BackEnd.Aplicacao.AplicUsuarios.Views;
using GerEsportes_BackEnd.Dominios.Dtos;
using GerEsportes_BackEnd.Dominios.Usuarios;
using GerEsportes_BackEnd.Dominios.Usuarios.Documentos;
using Microsoft.OpenApi.Extensions;

namespace GerEsportes_BackEnd.Dominios.Mappers
{
    public class UsuarioMapper
    {
        public static UsuarioView MapearUsuarioView(Usuario usuario)
        {
            UsuarioView userView = new UsuarioView();

            userView.Id = usuario.Id;
            userView.Email = usuario.Email;
            userView.Nome = usuario.Nome;
            userView.DataNascimento = usuario.DataNascimento;
            userView.Cargo = usuario.Cargo;
            userView.Telefone = usuario.Telefone;
            userView.Cref = usuario.Cref;
            userView.Ativo = usuario.Ativo; 
            userView.Federacao = usuario.Federacao;
            userView.TipoUsuario = usuario.TipoUsuario;
            userView.DocumentoUsuario = MapearDocumentoUsuarioView(usuario.DocumentoUsuario);
            userView.CpfRg = usuario.CpfRg;
            userView.Categoria = usuario.Categoria.GetDisplayName();
            userView.Modalidade = usuario.Modalidade.GetDisplayName();
            userView.Genero = usuario.Genero.GetDisplayName();
            userView.Time = usuario.Time.GetDisplayName();   

            return userView;
        }

        public static List<DocumentoUsuarioView> MapearDocumentoUsuarioView(List<DocumentoUsuario> documentos)
        {
            if (documentos == null)
                return null;
            
            var listView = new List<DocumentoUsuarioView>();    

            foreach (var docView in documentos)
            {
                var view = new DocumentoUsuarioView();

                view.Extensao = docView.Extensao;
                view.NomeDocumento = docView.NomeDocumento;
                view.GuidDocumento = docView.GuidDocumento;
                view.CodigoUsuario = docView.CodigoUsuario;
                view.ImagemPerfil = docView.ImagemPerfil;
                view.Id = docView.Id;

                listView.Add(view);
            }
            return listView;
        }

        public static Usuario MapearUsuario(UsuarioDto dto)
        {
            Usuario usuarioNovo = new Usuario();

            usuarioNovo.Nome = dto.Nome;
            usuarioNovo.Email = FormatacaoEmail(dto.Email);
            usuarioNovo.DataNascimento = dto.DataNascimento.Date;
            usuarioNovo.Cargo = dto.Cargo;
            usuarioNovo.Telefone = dto.Telefone;
            usuarioNovo.Cref = dto.Cref;
            usuarioNovo.Federacao = dto.Federacao;
            usuarioNovo.TipoUsuario = dto.TipoUsuario;
            usuarioNovo.CpfRg = dto.CpfRg;
            usuarioNovo.Categoria = dto.Categoria;
            usuarioNovo.Modalidade = dto.Modalidade;
            usuarioNovo.Genero = dto.Genero;
            usuarioNovo.Time = dto.Time; 

            usuarioNovo.Ativo = true;

            return usuarioNovo;
        }

        private static string FormatacaoEmail(string email)
        {
            string[] partes = email.Split(' ');

            string emailFormatado = string.Join("", partes);

            return emailFormatado.ToLower();
        }
    }
}
