using GerEsportes_BackEnd.Dominios.Dtos;
using GerEsportes_BackEnd.Dominios.Enuns;
using GerEsportes_BackEnd.Dominios.Usuarios;
using GerEsportes_BackEnd.Repositorios.Usuarios;
using Microsoft.OpenApi.Extensions;

namespace GerEsportes_BackEnd.Aplicacao.AplicDashboards
{
    public class AplicDashboard : IAplicDashboard
    {

        private readonly IRepUsuario _repUsuario;
        private readonly List<Usuario> _usuariosLista;

        public AplicDashboard(IRepUsuario repUsuario)
        {
            _repUsuario = repUsuario;
            _usuariosLista = _repUsuario.RecuperarTodos();
        }

        public List<DashboardPizzaDto> AtletasPorModalidde()
        {
            int quntBasquete = 0;
            int quntVolei = 0;
            int quntFutsal = 0;
            int quntHandeibol = 0;

            quntBasquete = _usuariosLista.Where(x => x.Modalidade == EnumModalidade.BASKET).Count();
            quntVolei = _usuariosLista.Where(x => x.Modalidade == EnumModalidade.VOLEI).Count();
            quntFutsal = _usuariosLista.Where(x => x.Modalidade == EnumModalidade.FUTSAL).Count();
            quntHandeibol = _usuariosLista.Where(x => x.Modalidade == EnumModalidade.HANDEBOL).Count();

            List<DashboardPizzaDto> lista = new List<DashboardPizzaDto>();

            var dtoBasquete = new DashboardPizzaDto()
            {
                NomeModalidade = "Basquete",
                QuantidadeAtletas = quntBasquete != null ? quntBasquete : 0,

            };
            var dtoVolei = new DashboardPizzaDto()
            {
                NomeModalidade = "Volei",
                QuantidadeAtletas = quntVolei != null ? quntVolei : 0,
            };
            var dtoFutsal = new DashboardPizzaDto()
            {
                NomeModalidade = "Futsal",
                QuantidadeAtletas = quntFutsal != null ? quntFutsal : 0,
            };
            var dtoHandeibol = new DashboardPizzaDto()
            {
                NomeModalidade = "Handebol",
                QuantidadeAtletas = quntHandeibol != null ? quntHandeibol : 0,
            };

            lista.Add(dtoBasquete);
            lista.Add(dtoVolei);
            lista.Add(dtoFutsal);
            lista.Add(dtoHandeibol);

            return lista;
        }

        public List<DashboardMediaIdadeDto> AtletasMediaIdade()
        {
            var hoje = DateTime.Today;

            var mediaIdadePorModalidade = _usuariosLista
                .GroupBy(u => u.Modalidade)
                .Select(g => new
                {
                    Modalidade = g.Key,
                    MediaIdade = g.Average(u => (hoje.Year - u.DataNascimento.Year) -
                                  (u.DataNascimento.Date > hoje.AddYears(-(hoje.Year - u.DataNascimento.Year)) ? 1 : 0))
                })
                .ToList();

            List<DashboardMediaIdadeDto> listaMedia = new List<DashboardMediaIdadeDto>();

            foreach (var item in mediaIdadePorModalidade)
            {
                var media = new DashboardMediaIdadeDto();

                media.Modalidade = item.Modalidade.GetDisplayName();
                media.MediaIdade = Math.Round(item.MediaIdade, 0);

                listaMedia.Add(media);
            }

            return listaMedia;
        }

        public List<DashbordPorCategoriaModalidadeDto> AtletasGeneroFeminino()
        {
            var lista = new List<DashbordPorCategoriaModalidadeDto>();

            foreach (var modlidade in _usuariosLista.Select(s => s.Modalidade).Distinct())
            {
                var usuarios = _usuariosLista
                    .Where(u => u.Modalidade == modlidade && u.Categoria == u.Categoria)
                    .GroupBy(u => u.Categoria)
                    .Select(g => new
                    {
                        Categoria = g.Key.ToString(),
                        Quantidade = g.Count()
                    })
                    .ToList();

                var quantidadeTotal = usuarios.Sum(u => u.Quantidade);

                var categorias = usuarios
                        .Select(u => $"{u.Categoria}, {u.Quantidade}")
                        .ToList();

                var dto = new DashbordPorCategoriaModalidadeDto
                {
                    Categoria = categorias,
                    Modalidade = modlidade.GetDisplayName(),
                };

                lista.Add(dto);
            }

            return lista;
        }

        public DashboardMediasEPorcentagensDto MediasEPorcentagens()
        {
            var hoje = DateTime.Today;

            var totalAtletas = _usuariosLista.Count();
            var totalHomens = _usuariosLista.Count(u => u.Genero == EnumGenero.MASCULINO);
            var totalMulheres = _usuariosLista.Count(u => u.Genero == EnumGenero.FEMININO);

            var mediaIdade = _usuariosLista
                .Select(u => (hoje.Year - u.DataNascimento.Year) -
                             (u.DataNascimento.Date > hoje.AddYears(-(hoje.Year - u.DataNascimento.Year)) ? 1 : 0))
                .Average();

            var porcentagemHomens = (totalHomens / (double)totalAtletas) * 100;
            var porcentagemMulheres = (totalMulheres / (double)totalAtletas) * 100;

            var resultado = new DashboardMediasEPorcentagensDto
            {
                TotalAtletas = totalAtletas,
                MediaIdade = Math.Round(mediaIdade, 0),
                PorcentagemHomens = Math.Round(porcentagemHomens, 0),
                PorcentagemMulheres = Math.Round(porcentagemMulheres, 0)
            };

            return resultado;
        }
    }
}
