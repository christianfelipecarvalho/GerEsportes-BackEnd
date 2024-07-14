
using GerEsportes_BackEnd.Dominios.Agendas;
using GerEsportes_BackEnd.Dominios.Usuarios;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GerEsportes_BackEnd.Repositorios.Base
{
    public class RepositoryModelBase<T> : IRepositoryModel<T>, IDisposable where T : class
    {

        protected Contexto _contexto;
        public bool _SaveChanges = true;
        protected DbSet<T> _dbSet;

        public RepositoryModelBase(Contexto contexto, bool saveChanges = true)
        {
            _SaveChanges = saveChanges;
            _contexto = contexto;
            _dbSet = _contexto.Set<T>();
        }

        //public IQueryable<T> Where(Expression<Func<T, bool>> exp)
        //{
        //    return _dbSet.Where(exp);
        //}

        public IQueryable<T> Where(Expression<Func<T, bool>> exp)
        {
            var query = _dbSet.Where(exp);
            var navigations = _contexto.Model.FindEntityType(typeof(T)).GetNavigations().ToList();

            if (navigations.Count > 1)
            {
                foreach (var navigation in navigations)
                {
                    query = query.Include(navigation.Name);
                }
            }

            return query;
        }

        public T Alterar(T objeto)
        {
            _contexto.Entry(objeto).State = EntityState.Modified;
           
            if(_SaveChanges)
                _contexto.SaveChanges();    

            return objeto;  
        }

        public void Dispose()
        {
            _contexto?.Dispose();   
        }

        public void Excluir(T objeto)
        {
            _contexto.Set<T>().Remove(objeto);

            if (_SaveChanges)
                _contexto.SaveChanges();
        }

        public void Excluir(params object[] variavel)
        {
            var obj = RecuperarPorId(variavel);
            Excluir(obj);
        }

        //public T Inserir(T objeto)
        //{
        //    _contexto.Set<T>().Add(objeto);

        //     if (_SaveChanges)
        //        _contexto.SaveChanges();

        //    return objeto;

        //}

        public T Inserir(T objeto)
        {
            _contexto.Set<T>().Add(objeto);

            // Verifica se há propriedades de navegação e inclui automaticamente
            var navigations = _contexto.Model.FindEntityType(typeof(T)).GetNavigations().ToList();

            if (navigations.Count > 1)
            {
                foreach (var navigation in navigations)
                {
                    _contexto.Entry(objeto).Reference(navigation.Name).Load();
                }
            }

            if (_SaveChanges)
                _contexto.SaveChanges();

            return objeto;
        }

        public void PersistirTransacao()
        {
            _contexto.SaveChanges(true);
        }

        public T RecuperarPorId(params object[] variavel)
        {
            return _contexto.Set<T>().Find(variavel);
        }

        //public T RecuperarPorId(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        //{
        //    var query = _contexto.Set<T>().AsQueryable();

        //    if (includes != null)
        //    {
        //        query = includes.Aggregate(query, (current, include) => current.Include(include));
        //    }

        //    return query.SingleOrDefault(predicate);
        //}

        public T RecuperarPorId(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var query = _dbSet.AsQueryable();

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            // Verifica se há propriedades de navegação e inclui automaticamente
            var navigations = _contexto.Model.FindEntityType(typeof(T)).GetNavigations().ToList();

            if (navigations.Count > 1)
            {
                foreach (var navigation in navigations)
                {
                    query = query.Include(navigation.Name);
                }
            }

            return query.SingleOrDefault(predicate);
        }



        //public List<T> RecuperarTodos()
        //{
        //    return _contexto.Set<T>().ToList<T>();
        //}

        public List<T> RecuperarTodos()
        {
            var query = _dbSet.AsQueryable();
            var navigations = _contexto.Model.FindEntityType(typeof(T)).GetNavigations().ToList();

            if (navigations.Count > 1)
            {
                foreach (var navigation in navigations)
                {
                    query = query.Include(navigation.Name);
                }
            }

            return query.ToList();
        }

        public List<Usuario> RecuperarTodosComDocumentos()
        {
            return _contexto.Set<Usuario>().Include(u => u.DocumentoUsuario).ToList();
        }
    }
}
