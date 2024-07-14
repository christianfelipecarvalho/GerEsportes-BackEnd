namespace GerEsportes_BackEnd.Dominios.Usuarios.Documentos
{
    public class DocumentoUsuario
    {
        public int Id { get; set; }
        public string NomeDocumento { get; set; }
        public string GuidDocumento { get; set; }
        public string Extensao { get; set; }
        public int CodigoUsuario { get; set; }
        public bool ImagemPerfil { get; set; }

        public Usuario Usuario { get; set; }
    }
}
