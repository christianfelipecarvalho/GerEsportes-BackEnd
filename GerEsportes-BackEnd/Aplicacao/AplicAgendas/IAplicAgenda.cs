using GerEsportes_BackEnd.Aplicacao.AplicAgendas.Views;
using GerEsportes_BackEnd.Dominios.Dtos;

namespace GerEsportes_BackEnd.Aplicacao.AplicAgendas
{
    public interface IAplicAgenda
    {
        List<AgendaView> SalvarAgenda(List<SalvarAgendaDto> dto);
        List<AgendaView> AlterarAgenda(List<AlterarAgendaDto> dtos);
        List<AgendaView> ListarTodasAgendas();
        AgendaView ListarAgendasPorId(int codigoUsuario, int codigoAgenda);
        void ExcluirAgenda(int codigoUsuario, int codigoAgenda);
    }
}
