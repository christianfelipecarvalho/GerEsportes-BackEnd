namespace GerEsportes_BackEnd.Aplicacao.AplicUsuarios.Views
{
    public class DocumentoUsuarioView
    {
        public int Id { get; set; }
        public string NomeDocumento { get; set; }
        public string GuidDocumento { get; set; }
        public string Extensao { get; set; }
        public int CodigoUsuario { get; set; }
        public bool ImagemPerfil { get; set; }
    }
}
