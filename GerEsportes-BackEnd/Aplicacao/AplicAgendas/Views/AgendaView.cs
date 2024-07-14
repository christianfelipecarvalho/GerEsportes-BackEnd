using GerEsportes_BackEnd.Dominios.Enuns;

namespace GerEsportes_BackEnd.Aplicacao.AplicAgendas.Views
{
    public class AgendaView
    {
        public int Id { get; set; }
        public string Modalidade { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string TipoEvento { get; set; }
        public int CodigoLocal { get; set; }
        public int CodigoUsuario { get; set; }
        public DateTime DataSalvamento { get; set; }
        public string Titulo { get; set; }
        public string  NomeUsuario { get; set; }
        public string DescricaoLocal { get; set; }
        public EnumCategoria Categoria { get; set; }
        public string Obs { get; set; }
    }
}
