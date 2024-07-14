using GerEsportes_BackEnd.Dominios.Usuarios;
using GerEsportes_BackEnd.Repositorios.Base;

namespace GerEsportes_BackEnd.Repositorios.Usuarios
{
    public class RepUsuario : RepositoryModelBase<Usuario>, IRepUsuario
    {
        public RepUsuario(Contexto contexto, bool saveChanges = true) : base(contexto, saveChanges)
        {
        }
    }
}
