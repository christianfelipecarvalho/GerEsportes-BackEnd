namespace GerEsportes_BackEnd.Dominios.Dtos
{
    public class SalvarSenhaDto
    {
        public int CodigoUsuario { get; set; }
        public string SenhaNova { get; set; }
        public string SenhaConfirmacao { get; set; }
    }
}
