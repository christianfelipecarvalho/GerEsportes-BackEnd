using GerEsportes_BackEnd.Dominios.Locais;
using GerEsportes_BackEnd.Repositorios.Base;

namespace GerEsportes_BackEnd.Repositorios.Locais
{
    public class RepLocal : RepositoryModelBase<Local>, IRepLocal
    {
        public RepLocal(Contexto contexto, bool saveChanges = true) : base(contexto, saveChanges)
        {
        }
    }
}
