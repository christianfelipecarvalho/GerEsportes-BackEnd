using GerEsportes_BackEnd.Dominios.Agendas;

namespace GerEsportes_BackEnd.Dominios.Locais
{
    public class Local
    {
        public int Id { get; set; }
        public string Rua { get; set; }
        public string Cidade { get; set; }
        public string Cep { get; set; }
        public string Complemento { get; set; }
        public string Numero { get; set; }
        public bool Ativo { get; set; }
        public string Descricao { get; set; }
    }
}
