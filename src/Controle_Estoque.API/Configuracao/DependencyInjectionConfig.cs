using Controle_Estoque.Aplicacao.Interfaces.Empresas;
using Controle_Estoque.Aplicacao.Interfaces.Filiais;
using Controle_Estoque.Aplicacao.Interfaces.Produtos;
using Controle_Estoque.Aplicacao.Servicos.Empresas;
using Controle_Estoque.Aplicacao.Servicos.Filiais;
using Controle_Estoque.Aplicacao.Servicos.Produtos;
using Controle_Estoque.Domain.Interfaces.Empresas;
using Controle_Estoque.Domain.Interfaces.Filiais;
using Controle_Estoque.Domain.Interfaces.Notificador;
using Controle_Estoque.Domain.Interfaces.Produtos;
using Controle_Estoque.Domain.Notificacoes;
using Controle_Estoque.Infra.Data.Context;
using Controle_Estoque.Repositorio.Repositorios.Empresas;
using Controle_Estoque.Repositorio.Repositorios.Filiais;
using Controle_Estoque.Repositorio.Repositorios.Produtos;

namespace Controle_Estoque.API.Configuracao
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            //Infra
            services.AddScoped<AppDbContext>();

            //Repositorio
            services.AddScoped<IEmpresaRepositorio, EmpresaRepositorio>();
            services.AddScoped<IFilialRepositorio, FilialRepositorio>();
            services.AddScoped<IProdutoRepositorio, ProdutoRepositorio>();

            //Servicos
            services.AddScoped<IEmpresaServicos, EmpresaServicos>();
            services.AddScoped<IFilialServicos, FilialServicos>();
            services.AddScoped<IProdutoServico, ProdutoServicos>();


            services.AddScoped<INotificador, Notificador>();

            return services;
        }
    }
}
