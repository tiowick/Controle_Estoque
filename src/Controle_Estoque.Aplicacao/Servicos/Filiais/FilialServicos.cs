using Controle_Estoque.Aplicacao.Interfaces.Filiais;
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

            filial.CNPJ = filial.CNPJ?.RetirarMascaraDocumento().VarcharToSQL();

            var resultado = await new FilialValidacao(_filialRepositorio).ValidateAsync(filial);
            if (!resultado.IsValid)
            {
                resultado.Errors.ForEach(e => Notificar(e.ErrorMessage));
                return;
            }

            await _filialRepositorio.Adicionar(filial);
        }


        public async Task Atualizar(Filial filial)
        {

            filial.CNPJ = filial.CNPJ?.RetirarMascaraDocumento().VarcharToSQL();

            var resultado = await new FilialValidacao(_filialRepositorio).ValidateAsync(filial);
            if (!resultado.IsValid)
            {
                resultado.Errors.ForEach(e => Notificar(e.ErrorMessage));
                return;
            }

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
