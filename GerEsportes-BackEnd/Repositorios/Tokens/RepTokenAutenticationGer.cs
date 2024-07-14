using GerEsportes_BackEnd.Dominios.Tokens;
using GerEsportes_BackEnd.Repositorios.Base;

namespace GerEsportes_BackEnd.Repositorios.Tokens
{
    public class RepTokenAutenticationGer : RepositoryModelBase<TokenAutenticationGer>, IRepTokenAutenticationGer
    {
        public RepTokenAutenticationGer(Contexto contexto, bool saveChanges = true) : base(contexto, saveChanges)
        {
        }
    }
}
