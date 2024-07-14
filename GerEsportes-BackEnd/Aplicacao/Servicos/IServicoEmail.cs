using GerEsportes_BackEnd.Dominios.Emails;

namespace GerEsportes_BackEnd.Aplicacao.Servicos
{
    public interface IServicoEmail
    {
        void EnviarEmail(Email email);
    }
}
