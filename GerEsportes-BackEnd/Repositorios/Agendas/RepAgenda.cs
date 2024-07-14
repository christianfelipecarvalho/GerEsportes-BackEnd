using GerEsportes_BackEnd.Dominios.Agendas;
using GerEsportes_BackEnd.Repositorios.Base;

namespace GerEsportes_BackEnd.Repositorios.Agendas
{
    public class RepAgenda : RepositoryModelBase<Agenda>, IRepAgenda
    {
        public RepAgenda(Contexto contexto, bool saveChanges = true) : base(contexto, saveChanges)
        {
        }

    }
}
