namespace GerEsportes_BackEnd.Dominios.Dtos
{
    public class UploadDocumentoDto
    {
        public string Data { get; set; }
        public string NomeArquivo { get; set; }
        public string Extencao { get; set; }
        public int CodigoUsuario { get; set; }
        public bool ImagemPerfil { get; set; }
    }
}
