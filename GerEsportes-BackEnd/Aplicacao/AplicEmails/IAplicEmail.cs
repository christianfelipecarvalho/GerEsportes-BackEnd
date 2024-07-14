using GerEsportes_BackEnd.Dominios.Dtos;

namespace GerEsportes_BackEnd.Aplicacao.AplicEmails
{
    public interface IAplicEmail
    {
        void EsqueciMinhaSenha(EmailDto dto);
        int ValidarCodigoRecuperacao(ValidarRecuperacaoDto dto);
    }
}
