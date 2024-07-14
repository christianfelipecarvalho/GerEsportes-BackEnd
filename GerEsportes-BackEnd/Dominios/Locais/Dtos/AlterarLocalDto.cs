namespace GerEsportes_BackEnd.Dominios.Locais.Dtos
{
    public class AlterarLocalDto
    {
        public int CodigoLocal { get; set; }
        public string Rua { get; set; }
        public string Cidade { get; set; }
        public string Cep { get; set; }
        public string Complemento { get; set; }
        public string Numero { get; set; }
        public string Descricao { get; set; }
    }
}
