using GerEsportes_BackEnd.Dominios.Usuarios.Documentos;
using GerEsportes_BackEnd.Repositorios.Base;

namespace GerEsportes_BackEnd.Repositorios.Usuarios.Documentos
{
    public class RepDocumentoUsuario : RepositoryModelBase<DocumentoUsuario>, IRepDocumentoUsuario
    {
        public RepDocumentoUsuario(Contexto contexto, bool saveChanges = true) : base(contexto, saveChanges)
        {
        }
    }
}
