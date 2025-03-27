using Controle_Estoque.Aplicacao.Interfaces.Empresas;
using Controle_Estoque.Domain.Entidades.Empresas;
using Controle_Estoque.Domain.Entidades.Filiais;
using Controle_Estoque.Domain.Entidades.Validacoes;
using Controle_Estoque.Domain.Interfaces.Empresas;
using Controle_Estoque.Domain.Interfaces.Filiais;
using Controle_Estoque.Domain.Interfaces.Notificador;
using Controle_Estoque.Entidades.Validacoes.Documento.Padronizar.Texto;
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

            empresa.CNPJ = empresa.CNPJ?.RetirarMascaraDocumento().VarcharToSQL();

            var resultado = await new EmpresaValidacao(_empresaRepositorio).ValidateAsync(empresa);
            if (!resultado.IsValid)
            {
                resultado.Errors.ForEach(e => Notificar(e.ErrorMessage));
                return;
            }

            await _empresaRepositorio.Adicionar(empresa);
        }

        public async Task Atualizar(Empresa empresa)
        {
            empresa.CNPJ = empresa.CNPJ?.RetirarMascaraDocumento().VarcharToSQL();

            var resultado = await new EmpresaValidacao(_empresaRepositorio).ValidateAsync(empresa);
            if (!resultado.IsValid)
            {
                resultado.Errors.ForEach(e => Notificar(e.ErrorMessage));
                return;
            }

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
