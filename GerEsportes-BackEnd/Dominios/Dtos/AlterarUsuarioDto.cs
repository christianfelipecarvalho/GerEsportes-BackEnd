using GerEsportes_BackEnd.Dominios.Enuns;

namespace GerEsportes_BackEnd.Dominios.Dtos
{
    public class AlterarUsuarioDto
    {
        public int CodigoUsuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Cargo { get; set; }
        public string Telefone { get; set; }
        public string Cref { get; set; }
        public string Federacao { get; set; }
        public EnumTipoUsuario TipoUsuario { get; set; }
        public EnumCategoria Categoria { get; set; }
        public EnumModalidade Modalidade { get; set; }
        public EnumTime Time { get; set; }
        public EnumGenero Genero { get; set; }
        public string CpfRg { get; set; }
        public bool Ativo { get; set; }
    }
}
