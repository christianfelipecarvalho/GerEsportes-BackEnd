using GerEsportes_BackEnd.Dominios.Agendas;
using GerEsportes_BackEnd.Dominios.Enuns;
using GerEsportes_BackEnd.Dominios.Usuarios.Documentos;
using System.ComponentModel.DataAnnotations.Schema;

namespace GerEsportes_BackEnd.Dominios.Usuarios
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string? Senha { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Cargo { get; set; }
        public string Telefone { get; set; }
        public string Cref { get; set; }
        public string Federacao { get; set; }
        public EnumTipoUsuario TipoUsuario { get; set; }
        public EnumModalidade Modalidade { get; set; }
        public EnumCategoria Categoria { get; set; }
        public EnumTime Time { get; set; }
        public EnumGenero Genero { get; set; }
        public bool Ativo { get; set; }
        public string CpfRg { get; set; }

        public List<DocumentoUsuario> DocumentoUsuario { get; set; }

    }
}
