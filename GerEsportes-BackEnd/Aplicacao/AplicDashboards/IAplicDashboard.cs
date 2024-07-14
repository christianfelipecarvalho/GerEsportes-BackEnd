using GerEsportes_BackEnd.Dominios.Dtos;

namespace GerEsportes_BackEnd.Aplicacao.AplicDashboards
{
    public interface IAplicDashboard
    {
        List<DashboardPizzaDto> AtletasPorModalidde();
        List<DashboardMediaIdadeDto> AtletasMediaIdade();
        List<DashbordPorCategoriaModalidadeDto> AtletasGeneroFeminino();
        DashboardMediasEPorcentagensDto MediasEPorcentagens();
    }
}
