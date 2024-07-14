using GerEsportes_BackEnd.Aplicacao.AplicUsuarios.Views;
using GerEsportes_BackEnd.Aplicacao.Verificacoes;
using GerEsportes_BackEnd.Dominios.Dtos;
using GerEsportes_BackEnd.Dominios.Enuns;
using GerEsportes_BackEnd.Dominios.Mappers;
using GerEsportes_BackEnd.Dominios.Usuarios;
using GerEsportes_BackEnd.Dominios.Usuarios.Documentos;
using GerEsportes_BackEnd.Infra.Documentos;
using GerEsportes_BackEnd.Infra.Exceptions;
using GerEsportes_BackEnd.Repositorios.Usuarios;
using GerEsportes_BackEnd.Repositorios.Usuarios.Documentos;
using System;

namespace GerEsportes_BackEnd.Aplicacao.AplicUsuarios
{
    public class AplicUsuario : IAplicUsuario
    {
        private readonly IRepUsuario _repUsuario;
        private readonly IRepDocumentoUsuario _repDocumentoUsuario;

        public AplicUsuario(IRepUsuario repUsuario, IRepDocumentoUsuario documentoUsuario)
        {
            _repUsuario = repUsuario;
            _repDocumentoUsuario = documentoUsuario;
        }

        public UsuarioView ListarUsuario(int id)
        {
            var usuario = _repUsuario.RecuperarPorId(u => u.Id == id, u => u.DocumentoUsuario).gExceptionSeNull($"Usuário código {id} não encontrado!");

            var usuarioView = UsuarioMapper.MapearUsuarioView(usuario);

            byte[] imagemBites = RecuperarImagemPerfil(usuario.Id);

            if (imagemBites != null)
                usuarioView.ImagemPerfilBase64 = Convert.ToBase64String(imagemBites);

            return usuarioView;
        }

        public void SalvarUsuario(UsuarioDto dto, int codigoUsuarioLogado)
        {
            Verificador.ValidaModalidadeUsuario(_repUsuario, codigoUsuarioLogado, dto.Modalidade);

            dto.gExceptionSeNull("Informações de cadastro não encontradas!");

            var ret = _repUsuario.RecuperarTodos();

            var usuario = ret.Where(p => p.Email.Equals(dto.Email.ToLower())).FirstOrDefault();

            var usuarioCpfRg = ret.Where(p => p.CpfRg.Equals(dto.CpfRg)).FirstOrDefault();

            if (usuario != null) throw new Exception($"Usuário com o email {dto.Email} já está cadastrado!");

            if (usuarioCpfRg != null) throw new Exception($"Usuário com o RG/CPF {dto.CpfRg} já está cadastrado!");

            dto.Nome.gExceptionSeNull("Campo Nome não pode estar vazio!");
            dto.Email.gExceptionSeNull("Campo Email não pode estar vazio!");

            _repUsuario.Inserir(UsuarioMapper.MapearUsuario(dto));
        }

        public UsuarioView AlterarUsuario(AlterarUsuarioDto dto, int codigoUsuarioLogado)
        {
            Verificador.ValidaModalidadeUsuario(_repUsuario, codigoUsuarioLogado, dto.Modalidade);

            var usuario = _repUsuario.RecuperarPorId(dto.CodigoUsuario)
                            .gExceptionSeNull($"Usuário código {dto.CodigoUsuario} não encontrado!");

            var usuarioCpfRgCadatrado = _repUsuario.RecuperarTodos().Where(x => x.CpfRg.Equals(dto.CpfRg) && x.Id != dto.CodigoUsuario).FirstOrDefault();

            var usuarioEmailCadatrado = _repUsuario.RecuperarTodos().Where(x => x.Email.Equals(dto.Email) && x.Id != dto.CodigoUsuario).FirstOrDefault();

            if (usuarioCpfRgCadatrado != null)
                throw new Exception($"Usuário com o RG/CPF {dto.CpfRg} já está cadastrado!");

            if (usuarioEmailCadatrado != null)
                throw new Exception($"Usuário com o Email {dto.Email} já está cadastrado!");

            usuario.Email = dto.Email;
            usuario.Nome = dto.Nome;
            usuario.DataNascimento = dto.DataNascimento;
            usuario.Cargo = dto.Cargo;
            usuario.Telefone = dto.Telefone;
            usuario.Cref = dto.Cref;
            usuario.Federacao = dto.Federacao;
            usuario.TipoUsuario = dto.TipoUsuario;
            usuario.CpfRg = dto.CpfRg;
            usuario.Categoria = dto.Categoria;
            usuario.Modalidade = dto.Modalidade;
            usuario.Ativo = dto.Ativo;
            usuario.Genero = dto.Genero;
            usuario.Time = dto.Time;

            _repUsuario.Alterar(usuario);

            return UsuarioMapper.MapearUsuarioView(usuario);
        }

        public List<UsuarioView> ListarTodosUsuarios(int codigoUsuarioLogado)
        {
            var usuarioLogado = _repUsuario.RecuperarPorId(codigoUsuarioLogado);

            List<Usuario> usuarios = new List<Usuario>();

            if (usuarioLogado.TipoUsuario == EnumTipoUsuario.ADMINISTRADOR)
            {
                var usuariosAdmin = _repUsuario.RecuperarTodosComDocumentos();

                usuarios = usuariosAdmin;
            }
            else
            {
                var usuariosModalidade = _repUsuario.RecuperarTodosComDocumentos().Where(w => w.Modalidade == usuarioLogado.Modalidade).ToList();

                usuarios = usuariosModalidade;
            }

            List<UsuarioView> view = new List<UsuarioView>();

            foreach (var ret in usuarios)
            {
                var user = UsuarioMapper.MapearUsuarioView(ret);

                foreach (var imagem in ret.DocumentoUsuario)
                {
                    byte[] imagemBites = RecuperarImagemPerfil(ret.Id);

                    if (imagemBites != null)
                        user.ImagemPerfilBase64 = Convert.ToBase64String(imagemBites);
                }

                view.Add(user);
            }
            view = view.OrderBy(x => x.Nome).ToList();

            view = view.OrderByDescending(x => x.Ativo).ToList();

            return view;
        }

        public void InativarUsuario(int id, int codigoUsuarioLogado)
        {
            var usuarioLogado = _repUsuario.RecuperarPorId(codigoUsuarioLogado);

            var usuario = _repUsuario.RecuperarPorId(id).gExceptionSeNull($"Usuário código {id} não encontrado!");

            if (usuarioLogado.TipoUsuario != EnumTipoUsuario.ADMINISTRADOR)
            {
                if (usuario.Modalidade != usuarioLogado.Modalidade)
                    throw new Exception($"Usuário não possui permissão para intivar usuario dessa modalidade!");
            }

            usuario.Ativo = usuario.Ativo == false ? true : false;

            _repUsuario.Alterar(usuario);
        }

        public void SalvarSenha(SalvarSenhaDto dto)
        {
            var usuario = _repUsuario.RecuperarPorId(dto.CodigoUsuario).gExceptionSeNull($"Usuário código {dto.CodigoUsuario} não encontrado!");

            if (!dto.SenhaConfirmacao.Equals(dto.SenhaNova))
                throw new Exception("Senhas não coincidem!");

            usuario.Senha = dto.SenhaNova;

            _repUsuario.Alterar(usuario);
        }

        public string UploadDocumento(UploadDocumentoDto dto, int codigoUsuarioLogado)
        {
            dto.gExceptionSeNull("Arquivo não pode ser salvo!");
            dto.Data.gExceptionSeNull("O arquivo é vazio e não pode ser salvo!");

            var usuario = _repUsuario.RecuperarPorId(dto.CodigoUsuario).gExceptionSeNull($"Usuário código {dto.CodigoUsuario} não encontrado!");

            Verificador.ValidaModalidadeUsuario(_repUsuario, codigoUsuarioLogado, usuario.Modalidade);

            var documentoUsuarioExistente = _repDocumentoUsuario.RecuperarTodos().Where(x => x.CodigoUsuario == usuario.Id && x.ImagemPerfil).FirstOrDefault();

            DocumentoUsuario documentoUsuario = new DocumentoUsuario();

            FileControll upload = new FileControll();

            var guide = Guid.NewGuid().ToString();

            upload.UploadBase64(dto.Data, guide);

            documentoUsuario.GuidDocumento = guide;
            documentoUsuario.Extensao = dto.Extencao;
            documentoUsuario.NomeDocumento = dto.NomeArquivo;
            documentoUsuario.CodigoUsuario = usuario.Id;
            documentoUsuario.ImagemPerfil = dto.ImagemPerfil;

            if (documentoUsuarioExistente != null && dto.ImagemPerfil)
                _repDocumentoUsuario.Excluir(documentoUsuarioExistente);

            _repDocumentoUsuario.Inserir(documentoUsuario);

            if (usuario.DocumentoUsuario == null)
                usuario.DocumentoUsuario = new List<DocumentoUsuario>();

            usuario.DocumentoUsuario.Add(documentoUsuario);

            _repUsuario.Alterar(usuario);

            return "Documento salvo com sucesso!";
        }

        public string DownloadArquivo(int id)
        {
            var documento = _repDocumentoUsuario.RecuperarPorId(id).gExceptionSeNull($"Documento código {id} não encontrado!");

            FileControll arquivo = new FileControll();

            var arquivoBase64 = arquivo.RecuperarBase64Arquivo(documento.GuidDocumento).Result;

            return Convert.ToBase64String(arquivoBase64);
        }

        public void DeletarArquivo(int id, int codigoUsuarioLogado)
        {
            var documento = _repDocumentoUsuario.RecuperarPorId(id).gExceptionSeNull($"Documento código {id} não encontrado!");

            var usuarioDocumento = _repUsuario.RecuperarPorId(documento.CodigoUsuario).gExceptionSeNull($"Usuário do documento código {documento.Id} não encontrado!");

            Verificador.ValidaModalidadeUsuario(_repUsuario, codigoUsuarioLogado, usuarioDocumento.Modalidade);

            FileControll arquivo = new FileControll();

            arquivo.DeletarArquivo(documento.GuidDocumento);

            _repDocumentoUsuario.Excluir(documento);
        }

        public byte[] RecuperarImagemPerfil(int id)
        {
            var documento = _repDocumentoUsuario.RecuperarTodos().Where(i => i.ImagemPerfil && i.CodigoUsuario == id).FirstOrDefault();

            if (documento == null) return null;

            FileControll arquivo = new FileControll();

            return arquivo.RecuperarBase64Arquivo(documento.GuidDocumento).Result;
        }

        public List<UsuarioView> ListarUsuarioPorModalidade(int id)
        {
            var usuario = _repUsuario.RecuperarPorId(id).gExceptionSeNull($"Usuário código {id} não encontrado!");

            var modalidadeUsuario = usuario.Modalidade;

            var usuarioPorModalidade = _repUsuario.Where(w => w.Modalidade == modalidadeUsuario).ToList();

            List<UsuarioView> view = new List<UsuarioView>();

            usuarioPorModalidade.ForEach(p =>
            {
                view.Add(UsuarioMapper.MapearUsuarioView(p));
            });

            return view;
        }
    }
}
