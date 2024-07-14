using GerEsportes_BackEnd.Aplicacao.AplicLocais.Views;
using GerEsportes_BackEnd.Dominios.Locais;
using GerEsportes_BackEnd.Dominios.Locais.Dtos;

namespace GerEsportes_BackEnd.Dominios.Mappers
{
    public class LocalMapper
    {
        public static LocalGerView MapearLocalView(Local local)
        {
            LocalGerView localView = new LocalGerView();

            localView.CodigoLocal = local.Id;
            localView.Cidade = local.Cidade;
            localView.Rua = local.Rua;
            localView.Complemento = local.Complemento;
            localView.Cep = local.Cep;
            localView.Numero = local.Numero;
            localView.Ativo = local.Ativo;
            localView.Descricao = local.Descricao;  

            return  localView;
        }

        public static Local MapearLocal(LocalDto dto)
        {
            Local novoLocal = new Local(); 
            
            novoLocal.Cidade = dto.Cidade;
            novoLocal.Rua = dto.Rua;
            novoLocal.Complemento = dto.Complemento;
            novoLocal.Cep = dto.Cep;
            novoLocal.Numero = dto.Numero;
            novoLocal.Descricao = dto.Descricao;

            novoLocal.Ativo = true;

            return novoLocal;
        }
    }
}
