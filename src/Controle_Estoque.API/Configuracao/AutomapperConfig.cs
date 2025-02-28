using AutoMapper;
using Controle_Estoque.API.Modulos.Empresas.ViewModels;
using Controle_Estoque.API.Modulos.Filiais.ViewModels;
using Controle_Estoque.Domain.Entidades.Empresas;
using Controle_Estoque.Domain.Entidades.Filiais;

namespace Controle_Estoque.API.Configuracao
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Empresa, EmpresaCreateViewModel>().ReverseMap();

            CreateMap<Filial, FilialCreateViewModel>().ReverseMap();


        }
    }
}
