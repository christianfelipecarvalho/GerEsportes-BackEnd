using GerEsportes_BackEnd.Aplicacao.AplicAgendas.Views;
using GerEsportes_BackEnd.Aplicacao.Verificacoes;
using GerEsportes_BackEnd.Dominios.Dtos;
using GerEsportes_BackEnd.Dominios.Enuns;
using GerEsportes_BackEnd.Dominios.Mappers;
using GerEsportes_BackEnd.Infra.Exceptions;
using GerEsportes_BackEnd.Repositorios.Agendas;
using GerEsportes_BackEnd.Repositorios.Usuarios;

namespace GerEsportes_BackEnd.Aplicacao.AplicAgendas
{
    public class AplicAgenda : IAplicAgenda
    {
        private readonly IRepAgenda _repAgenda;
        private readonly IRepUsuario _repUsuario;

        public AplicAgenda(IRepAgenda repAgenda, IRepUsuario usuario)
        {
            _repAgenda = repAgenda;
            _repUsuario = usuario;
        }

        public List<AgendaView> SalvarAgenda(List<SalvarAgendaDto> dto)
        {
            List<AgendaView> viewAgenda = new List<AgendaView>();

            dto.ForEach(a =>
            {
                Verificador.ValidaModalidadeUsuario(_repUsuario, a.CodigoUsuario, a.Modalidade);

                var agenda = _repAgenda.Inserir(AgendaMapper.MapearAgenda(a));

                viewAgenda.Add(AgendaMapper.MapearAgendaView(agenda));
            });

            return viewAgenda;
        }

        public List<AgendaView> AlterarAgenda(List<AlterarAgendaDto> dtos)
        {
            List<AgendaView> viewAgenda = new List<AgendaView>();

            foreach (var dto in dtos)
            {
                var agenda = _repAgenda.RecuperarPorId(dto.CodigoAgenda)
                    .gExceptionSeNull($"Agenda com o código {dto.CodigoAgenda} não encontrado!");

                Verificador.ValidaModalidadeUsuario(_repUsuario, dto.CodigoUsuario, dto.Modalidade);

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

                _repAgenda.Alterar(agenda);

                viewAgenda.Add(AgendaMapper.MapearAgendaView(agenda));
            }

            return viewAgenda;
        }

        public List<AgendaView> ListarTodasAgendas()
        {
            var agenda = _repAgenda.RecuperarTodos();

            List<AgendaView> agendaView = new List<AgendaView>();

            agenda.ForEach(a =>
            {
                agendaView.Add(AgendaMapper.MapearAgendaView(a));
            });

            return agendaView;
        }

        public AgendaView ListarAgendasPorId(int codigoUsuario, int codigoAgenda)
        {
            var usuario = _repUsuario.RecuperarPorId(codigoUsuario);

            var agenda = _repAgenda.RecuperarPorId(codigoAgenda);

            if (agenda.Modalidade != usuario.Modalidade && usuario.TipoUsuario != EnumTipoUsuario.ADMINISTRADOR)
                throw new Exception($"Usuário não possui permisão para agenda de código {codigoAgenda}! ");

            if (agenda == null)
                throw new Exception($"Nenhuma agenda com o código {codigoAgenda} foi encontrada!");

            return AgendaMapper.MapearAgendaView(agenda);
        }

        public void ExcluirAgenda(int codigoUsuario, int codigoAgenda)
        {
            var usuario = _repUsuario.RecuperarPorId(codigoUsuario);

            var agenda = _repAgenda
                .Where(w => w.Modalidade == usuario.Modalidade && w.Id == codigoAgenda || usuario.TipoUsuario == EnumTipoUsuario.ADMINISTRADOR)
                .FirstOrDefault()
                .gExceptionSeNull($"Angeda código {codigoAgenda} não encontrada ou usuário não possui permissão!");

            _repAgenda.Excluir(agenda);
        }
    }
}