using Controle_Estoque.Aplicacao.Interfaces.Estoques;
using Controle_Estoque.Domain.Entidades.Empresas;
using Controle_Estoque.Domain.Entidades.Estoques;
using Controle_Estoque.Domain.Entidades.Validacoes;
using Controle_Estoque.Domain.Interfaces.Estoques;
using Controle_Estoque.Domain.Interfaces.Notificador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle_Estoque.Aplicacao.Servicos.Estoques
{
    public class EstoqueServicos : BaseServices, IEstoqueServicos
    {

        private readonly IEstoqueRepositorio _estoqueRepositorio;

        public EstoqueServicos( IEstoqueRepositorio estoqueRepositorio, 
            INotificador notificador) : base(notificador)
        {
            _estoqueRepositorio = estoqueRepositorio;
        }

        public async Task Adicionar(Estoque estoque)
        {
            if (!ExecutarValidacao(new EstoqueValidacao(), estoque)) return;


            await _estoqueRepositorio.Adicionar(estoque);
        }

        public async Task Atualizar(Estoque estoque)
        {
            if (!ExecutarValidacao(new EstoqueValidacao(), estoque)) return;


            await _estoqueRepositorio.Adicionar(estoque); ;
        }

        public async Task Remover(Guid id)
        {
            await _estoqueRepositorio.Remover(id);
        }

        public void Dispose()
        {
            _estoqueRepositorio?.Dispose();
        }
    }
}
