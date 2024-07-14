using GerEsportes_BackEnd.Dominios.Agendas;
using GerEsportes_BackEnd.Dominios.Usuarios;
using System.Linq.Expressions;

namespace GerEsportes_BackEnd.Repositorios.Base
{
    public interface IRepositoryModel<T> where T : class
    {
        List<T> RecuperarTodos();
        T RecuperarPorId(params object[] variavel);
        T RecuperarPorId(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        T Inserir(T objeto);
        T Alterar(T objeto);
        void Excluir(T objeto);
        void Excluir(params object[] variavel);
        void PersistirTransacao();
        List<Usuario> RecuperarTodosComDocumentos();
        IQueryable<T> Where(Expression<Func<T, bool>> exp);
    }
}
