using Controle_Estoque.Aplicacao.Interfaces.Filiais;
using Controle_Estoque.Domain.Entidades.Filiais;
using Controle_Estoque.Domain.Entidades.Validacoes;
using Controle_Estoque.Domain.Interfaces.Empresas;
using Controle_Estoque.Domain.Interfaces.Filiais;
using Controle_Estoque.Domain.Interfaces.Notificador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle_Estoque.Aplicacao.Servicos.Filiais
{
    public class FilialServicos : BaseServices, IFilialServicos
    {
        private readonly IFilialRepositorio _filialRepositorio;

        public FilialServicos(IFilialRepositorio filialRepositorio, 
            INotificador notificador) : base(notificador)
        {
            _filialRepositorio = filialRepositorio;
            
        }


        public async Task Adicionar(Filial filial)
        {
            if (!ExecutarValidacao(new FilialValidacao(), filial)) return;

            var filialExistente = await _filialRepositorio.ObterPorId(filial.Id);
            if (filialExistente != null)
            {
                Notificar("Já existe uma filial com esse ID informado.");
                return;
            }
           

            var cnpjExistente = await _filialRepositorio.ObterPorCNPJ(filial.CNPJ);
            if (cnpjExistente != null)
            {
                Notificar("Já existe uma filial com esse CNPJ informado.");
                return;
            }

            var empresaExistente = await _filialRepositorio.ObterFiliaisPorEmpresa(filial.EmpresaId);
            if (cnpjExistente != null)
            {
                Notificar("A empresa informada não existe.");
                return;
            }

            await _filialRepositorio.Adicionar(filial);
        }


        public async Task Atualizar(Filial filial)
        {

            if (!ExecutarValidacao(new FilialValidacao(), filial)) return;

            await _filialRepositorio.Atualizar(filial);
        }


        public async Task Remover(Guid id)
        {
            await _filialRepositorio.Remover(id);
        }

        public void Dispose()
        {
            _filialRepositorio?.Dispose();
        }

       
    }
}
