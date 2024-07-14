using System.ComponentModel;

namespace GerEsportes_BackEnd.Dominios.Enuns
{
    public enum EnumTipoUsuario
    {
        [Description("TECNICO")]
        TECNICO,

        [Description("ATLETA")]
        ATLETA,

        [Description("ADMINISTRADOR")]
        ADMINISTRADOR
    }
}
