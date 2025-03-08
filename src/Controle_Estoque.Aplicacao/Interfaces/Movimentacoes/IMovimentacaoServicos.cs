using Controle_Estoque.Domain.Entidades.Movimentacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controle_Estoque.Aplicacao.Interfaces.Movimentacoes
{
    public interface IMovimentacaoServicos : IDisposable
    {
        Task RegistrarMovimentacao(Movimentacao movimentacao);
        Task<bool> ValidarMovimentacao(Movimentacao movimentacao);
    }
}
