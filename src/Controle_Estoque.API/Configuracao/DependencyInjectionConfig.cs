using Controle_Estoque.Aplicacao.Interfaces;
using Controle_Estoque.Aplicacao.Servicos;
using Controle_Estoque.Domain.Interfaces.Empresas;
using Controle_Estoque.Domain.Interfaces.Notificador;
using Controle_Estoque.Domain.Notificacoes;
using Controle_Estoque.Infra.Data.Context;
using Controle_Estoque.Repositorio.Repositorios.Empresas;

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

            services.AddScoped<IEmpresaServicos, EmpresaServicos>();


            services.AddScoped<INotificador, Notificador>();

            return services;
        }
    }
}
