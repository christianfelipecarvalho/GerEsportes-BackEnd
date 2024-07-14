using GerEsportes_BackEnd.Dominios.Enuns;
using GerEsportes_BackEnd.Repositorios.Usuarios;

namespace GerEsportes_BackEnd.Aplicacao.Verificacoes
{
    public class Verificador
    {
        public static bool ValidaModalidadeUsuario(IRepUsuario rep, int codigoUsuarioLogado, EnumModalidade modalidade)
        {
            var usuarioLogado = rep.RecuperarPorId(codigoUsuarioLogado);

            if (usuarioLogado.TipoUsuario == EnumTipoUsuario.ADMINISTRADOR)
                return true;

            if (modalidade != usuarioLogado.Modalidade)
                throw new Exception($"Usuário não possui permissão para alterações na modalidade {modalidade}!");

            return true;
        }
    }
}
