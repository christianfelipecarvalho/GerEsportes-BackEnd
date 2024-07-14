using GerEsportes_BackEnd.Dominios.Enuns;

namespace GerEsportes_BackEnd.Aplicacao.AplicUsuarios.Views
{
    public class UsuarioView
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Cargo { get; set; }
        public string Telefone { get; set; }
        public string Cref { get; set; }
        public bool Ativo { get; set; }
        public string Federacao { get; set; }
        public EnumTipoUsuario TipoUsuario { get; set; }
        public string Time { get; set; }
        public string Genero { get; set; }
        public string Categoria { get; set; }
        public string Modalidade { get; set; }
        public List<DocumentoUsuarioView> DocumentoUsuario { get; set; }
        public string ImagemPerfilBase64 { get; set; }
        public string CpfRg { get; set; }
    }
}
