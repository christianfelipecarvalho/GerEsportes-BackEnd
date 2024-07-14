using GerEsportes_BackEnd.Aplicacao.AplicAgendas.Views;
using GerEsportes_BackEnd.Dominios.Agendas;
using GerEsportes_BackEnd.Dominios.Dtos;
using Microsoft.OpenApi.Extensions;

namespace GerEsportes_BackEnd.Dominios.Mappers
{
    public class AgendaMapper
    {
        public static Agenda MapearAgenda(SalvarAgendaDto dto)
        {
            Agenda agenda = new Agenda();

            agenda.DataSalvamento = dto.DataSalvamento;
            agenda.Modalidade = dto.Modalidade;
            agenda.DataInicio = dto.DataInicio;
            agenda.DataFim = dto.DataFim;
            agenda.CodigoLocal = dto.CodigoLocal;
            agenda.CodigoUsuario = dto.CodigoUsuario;
            agenda.TipoEvento = dto.TipoEvento;
            agenda.Titulo = dto.Titulo;
            agenda.Categoria = dto.Categoria;
            agenda.Obs = dto.Obs;

            return agenda;
        }

        public static AgendaView MapearAgendaView(Agenda agenda)
        {
            AgendaView agendaView = new AgendaView();

            agendaView.Id = agenda.Id;
            agendaView.DataSalvamento = agenda.DataSalvamento;
            agendaView.Modalidade = agenda.Modalidade.GetDisplayName();
            agendaView.DataInicio = agenda.DataInicio;
            agendaView.DataFim = agenda.DataFim;
            agendaView.CodigoLocal = agenda.CodigoLocal;
            agendaView.CodigoUsuario = agenda.CodigoUsuario;
            agendaView.TipoEvento = agenda.TipoEvento;
            agendaView.Titulo = agenda.Titulo;
            agendaView.NomeUsuario = agenda.Usuario?.Nome;
            agendaView.DescricaoLocal = agenda.Local?.Descricao;
            agendaView.Categoria = agenda.Categoria;
            agendaView.Obs = agenda.Obs;

            return agendaView;
        }
    }
}
