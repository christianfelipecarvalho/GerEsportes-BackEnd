using GerEsportes_BackEnd.Aplicacao.AplicLocais.Views;
using GerEsportes_BackEnd.Dominios.Locais.Dtos;
using GerEsportes_BackEnd.Dominios.Mappers;
using GerEsportes_BackEnd.Infra.Exceptions;
using GerEsportes_BackEnd.Repositorios.Locais;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GerEsportes_BackEnd.Aplicacao.AplicLocais
{
    public class AplicLocal : IAplicLocal
    {

        private readonly IRepLocal _repLocal;

        public AplicLocal(IRepLocal repLocal)
        {
            _repLocal = repLocal;
        }

        public LocalGerView ListarLocal(int id)
        {
            var local = _repLocal.RecuperarPorId(id);

            if(local != null)
               return LocalMapper.MapearLocalView(local);

            return null;
        }

        public List<LocalGerView> ListarTodosLocais()
        {
            var locais = _repLocal.RecuperarTodos();

            var listaLocais = new List<LocalGerView>();

            foreach(var ret in locais)
            {
               var localView = LocalMapper.MapearLocalView(ret);
                listaLocais.Add(localView);
            }

            listaLocais.OrderBy(x => x.CodigoLocal).ThenBy(x => x.Ativo == true);

            return listaLocais;
        }

        public void SalvarLocal(LocalDto dto)
        {
            _repLocal.Inserir(LocalMapper.MapearLocal(dto));
        }

        public LocalGerView AlterarLocal(AlterarLocalDto dto)
        {
            var local = _repLocal.RecuperarPorId(dto.CodigoLocal);

            local.Cidade = dto.Cidade;
            local.Rua = dto.Rua;
            local.Complemento = dto.Complemento;
            local.Cep = dto.Cep;
            local.Numero = dto.Numero;
            local.Descricao = dto.Descricao;

            var localAlterado = _repLocal.Alterar(local);

            return LocalMapper.MapearLocalView(localAlterado);
        }

        public void InativarLocal(int id)
        {
            var local = _repLocal.RecuperarPorId(id).gExceptionSeNull($"Local código {id} não encontrado!");

            local.Ativo = local.Ativo == false ? true : false;

            _repLocal.Alterar(local);
        }
    }
}
