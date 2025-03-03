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
            CreateMap<EmpresaCreateViewModel, Empresa>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());


            CreateMap<FilialCreateViewModel, Filial>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Empresa, EmpresaViewModel>().ReverseMap(); // Mapeamento para atualização, busca etc. pode manter o Id

            CreateMap<Filial, FilialViewModel>().ReverseMap();




        }
    }
}
