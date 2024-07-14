using GerEsportes_BackEnd.Aplicacao.AplicLocais.Views;
using GerEsportes_BackEnd.Dominios.Locais.Dtos;

namespace GerEsportes_BackEnd.Aplicacao.AplicLocais
{
    public interface IAplicLocal
    {
        void SalvarLocal(LocalDto dto);
        List<LocalGerView> ListarTodosLocais();
        LocalGerView ListarLocal(int id);
        void InativarLocal(int id);
        LocalGerView AlterarLocal(AlterarLocalDto dto);
    }
}
