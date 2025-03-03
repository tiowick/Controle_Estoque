using Controle_Estoque.Aplicacao.Interfaces.Empresas;
using Controle_Estoque.Domain.Entidades.Empresas;
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

namespace Controle_Estoque.Aplicacao.Servicos.Empresas
{
    public class EmpresaServicos : BaseServices, IEmpresaServicos
    {
        private readonly IEmpresaRepositorio _empresaRepositorio;


        public EmpresaServicos(IEmpresaRepositorio empresaRepositorio,
            INotificador notificador) : base(notificador)
        {
            _empresaRepositorio = empresaRepositorio;
        }

        public async Task Adicionar(Empresa empresa)
        {

            if (!ExecutarValidacao(new EmpresaValidacao(), empresa)) return;

            var empresaExistente = await _empresaRepositorio.ObterPorId(empresa.Id);
            if (empresaExistente != null)
            {
                Notificar("Ja existe uma empresa com ID informado!");
                return;
            }

            var cnpjExistente = await _empresaRepositorio.ObterPorCNPJ(empresa.CNPJ);
            if (cnpjExistente != null)
            {
                Notificar("Já existe uma empresa com esse CNPJ informado.");
                return;
            }


            await _empresaRepositorio.Adicionar(empresa);
        }

        public async Task Atualizar(Empresa empresa)
        {

            if (!ExecutarValidacao(new EmpresaValidacao(), empresa)) return;

            await _empresaRepositorio.Atualizar(empresa);
        }

        public async Task Remover(Guid id)
        {
            await _empresaRepositorio.Remover(id);
        }

        public void Dispose()
        {
            _empresaRepositorio?.Dispose();
        }

       
    }
}
