using GerEsportes_BackEnd.Dominios.Emails;
using GerEsportes_BackEnd.Repositorios.Base;

namespace GerEsportes_BackEnd.Repositorios.Emails
{
    public class RepEmail : RepositoryModelBase<Email>, IRepEmail
    {
        public RepEmail(Contexto contexto, bool saveChanges = true) : base(contexto, saveChanges)
        {
        }

        public Task<Email> InserirEmail(Email email)
        {
            _contexto.Add(email);
            _contexto.SaveChanges();
            return null;
        }

        public Task EditarEmail(Email email)
        {
            _contexto.Update(email);
            _contexto.SaveChanges();

            return null;
        }

        public Email RecuperarPorId(int id)
        {
            return _contexto.Email.Find(id);
        }
    }
}
