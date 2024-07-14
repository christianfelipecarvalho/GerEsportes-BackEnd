using GerEsportes_BackEnd.Dominios.Pings;
using GerEsportes_BackEnd.Repositorios.Base;

namespace GerEsportes_BackEnd.Repositorios.Pings
{
    public class RepPing : RepositoryModelBase<PingEntity>, IRepPing
    {
        public RepPing(Contexto contexto, bool saveChanges = true) : base(contexto, saveChanges)
        {
        }
    }
}
