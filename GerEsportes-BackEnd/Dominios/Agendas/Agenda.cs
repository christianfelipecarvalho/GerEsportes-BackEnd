using GerEsportes_BackEnd.Dominios.Enuns;
using GerEsportes_BackEnd.Dominios.Locais;
using GerEsportes_BackEnd.Dominios.Usuarios;

namespace GerEsportes_BackEnd.Dominios.Agendas
{
    public class Agenda
    {
        public int Id { get; set; }
        public EnumModalidade Modalidade { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public string TipoEvento { get; set; }
        public int CodigoLocal { get; set; }
        public int CodigoUsuario { get; set; }
        public DateTime DataSalvamento { get; set; }
        public string Titulo { get; set; }
        public string? Obs { get; set; }
        public EnumCategoria Categoria { get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual Local Local { get; set; }
    }
}
