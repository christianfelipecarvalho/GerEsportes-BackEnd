using GerEsportes_BackEnd.Dominios.Enuns;

namespace GerEsportes_BackEnd.Dominios.Dtos
{
    public class SalvarAgendaDto
    {
        public EnumModalidade Modalidade { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string TipoEvento { get; set; }
        public int CodigoLocal { get; set; }
        public DateTime DataSalvamento { get; set; }
        public int CodigoUsuario { get; set; }
        public string Titulo { get; set; }
        public EnumCategoria Categoria { get; set; }
        public string Obs { get; set; }
    }
}
