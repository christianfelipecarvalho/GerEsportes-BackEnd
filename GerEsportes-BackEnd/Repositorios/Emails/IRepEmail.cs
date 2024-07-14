using GerEsportes_BackEnd.Dominios.Emails;
using GerEsportes_BackEnd.Repositorios.Base;

namespace GerEsportes_BackEnd.Repositorios.Emails
{
    public interface IRepEmail : IRepositoryModel<Email>
    {
        Email RecuperarPorId(int id);
        Task<Email> InserirEmail(Email email);
        Task EditarEmail(Email email);
    }
}
